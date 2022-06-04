using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Unity.GOAP.World;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Goal;
using Unity.GOAP.Planner;

namespace Unity.GOAP.Agent
{
    public class CAgent : MonoBehaviour
    {
        public string agentName = "agent Name";

        // Agent own belief system, or state that only the owner has access to.
        // This opposited to the world states, where all agent get access.
        public CFactManager agentFact;

        public List<CActionBase> actionList;
        public List<CGoal> goalList;
        public List<CGoal> goalBlacklist;

        [HideInInspector] public Vector3 position3D;

        protected List<CActionBase> possibleAction;
        protected Queue<CActionBase> actionQueue;
        protected CActionBase currentAction;
        protected CGoal currentGoal;

        protected CPlanner planner;

        bool interupt = false;

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            agentFact = new CFactManager();

            foreach (CActionBase a in actionList)
            {
                a.Initiate();
            }

            foreach (CGoal g in goalList)
            {
                g.Initiate();
            }
        }

        protected void AddAction(CActionBase action)
        {
            if (!this.actionList.Contains(action))
            {
                this.actionList.Add(action);
            }
        }
        protected void AddGoal(CGoal goal)
        {
            if (!this.goalList.Contains(goal))
            {
                this.goalList.Add(goal);
            }
        }

        // Called after t time to reset blacklist back to goal list
        protected void ResetBlackList()
        {
            foreach (CGoal g in goalBlacklist.ToList())
            {
                if (!goalList.Contains(g))
                {
                    goalList.Add(g);
                }
                goalBlacklist.Remove(g);
            }
        }

        protected void BlackListingGoal(CGoal goal)
        {
            if (!goalBlacklist.Contains(goal) && goal != null)
            {
                goalBlacklist.Add(goal);
            }
        }


        // Function to create a new plan (action queue)
        protected virtual void GetAGoal()
        {
            planner = new CPlanner();
            currentGoal = null;
            var sortedGoal = goalList.OrderByDescending(g => g.important);

            foreach (CGoal g in sortedGoal)
            {
                List<CActionBase> alist = new List<CActionBase>(actionList);
                actionQueue = planner.Plan(g, alist, agentFact);
                if (actionQueue != null)
                {
                    currentGoal = g;
                    Debug.Log("Agent: " + agentName + " start for goal: " + currentGoal.goalName);
                    break;
                }
            }

        }

        protected virtual void AskForInterupt() {
            throw new System.NotImplementedException();
        }

        protected virtual void PerformAction(CActionBase action) 
        {
            // If the action is performable by checking Pre_performing calculation, default always return true
            if (currentAction.Pre_Perform(this))
            {
                Debug.Log("Agent: " + agentName + " currently performing: " + currentAction.actionName);

                currentAction.PerformAction(this);
            }
            // If checking Pre_performing false, meaning the action is unable to perform for some reason, temporary remove 
            // the goal and re-plan.
            else
            {
                Debug.Log("Agent: " + agentName + " can not perform action: " + currentAction.actionName);
                goalList.Remove(currentGoal);
                BlackListingGoal(currentGoal);
                GetAGoal();
            }
            return;
        }

        float timer = 0f;
        protected virtual void FixedUpdate()
        {
            // After every x sec, reset the blacklist, this can be changed to different counter methods
            timer = timer += Time.deltaTime;
            if (timer >= 2f)
            {
                ResetBlackList();
                timer = 0;
            }

            // Check if currently running any action
            if ((currentAction != null) && (currentAction.isActive))
            {
                // If sometime when running, player or user ask to stop the action. stop and re-plan
                if ((interupt) && (currentAction.isInteruptable))
                {
                    currentAction = null;
                    GetAGoal();
                    return;
                }

                // If durring perfoming action, things happen that cause the action to fail, temporary remove 
                // the goal and re-plan.
                if (currentAction.HasFailed(this))
                {
                    Debug.Log("Agent: " + agentName + " fail to perform performing: " + currentAction.actionName);
                    goalList.Remove(currentGoal);
                    BlackListingGoal(currentGoal);
                    GetAGoal();
                    return;
                }

                // Check if the current action has complete yet?
                if (currentAction.HasCompleted(this))
                {
                    // Do pos calculation and switch to the next action
                    currentAction.Pos_Perform(this);
                    if (currentAction.forceReplan == true)
                    {
                        GetAGoal();
                        return;
                    }

                    // Should performing the action automatically add effect to the world state?
                    // If should, what will it be? World state or belief?
                    // As being undecided right now, the effect will be splited into 3

                    // Adding 
                    foreach (CFact f2 in currentAction.worldEffects)
                    {
                        CWorld.Instance.GetFacts().AddFact(f2);
                    }

                    foreach (CFact f2 in currentAction.agentEffects)
                    {
                        agentFact.AddFact(f2);
                    }
                    return;
                }
            }
            else
            {
                // If there is a goal to pursuit
                if (currentGoal != null)
                {
                    // Check if done all action is finished. For we planed the action sequence to satisfied the goal, if the 
                    // action sequence is complete, most likely the goal will be satisfied. therefore this should count as 
                    // checking the completation of the goal.
                    if (actionQueue.Count <= 0)
                    {
                        Debug.Log("Agent: " + agentName + " complete Goal: " + currentGoal.goalName);
                        // If goal is satisfied and non repeat, remove from the goal list
                        if (currentGoal.deletable)
                        {
                            goalList.Remove(currentGoal);
                        }
                        // And then find a new goal
                        GetAGoal();
                        return;
                    }
                    else
                    {
                        // Take 1 action from the plan queue, and execute it.
                        currentAction = actionQueue.Dequeue();
                        PerformAction(currentAction);
                        // Action is completed then remove the action from the action queue
                        return;
                    }
                }
                else
                {
                    GetAGoal();
                    return;
                }
            }

        }
    }

}
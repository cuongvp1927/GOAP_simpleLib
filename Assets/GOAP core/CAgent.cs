using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Unity.GOAP.World;
using Unity.GOAP.Action;
using Unity.GOAP.Goal;
using Unity.GOAP.Planner;


namespace Unity.GOAP.Agent
{
    public class CAgent : MonoBehaviour
    {
        public string agentName = "agent Name";

        //public List<CFact> agentFact;
        public CFactManager agentFact;

        public List<CActionBase> actionList;
        public List<CGoal> goalList;
        public List<CGoal> goalBlacklist;


        protected List<CActionBase> possibleAction;
        protected Queue<CActionBase> actionQueue;
        protected CActionBase currentAction;
        protected CGoal currentGoal;

        protected CPlanner planner;

        bool interupt = false;

        protected virtual void Awake()
        {
            ResetActionList();
            ResetGoalList();
            agentFact = new CFactManager();
        }

        protected virtual void Start()
        {
            Debug.Log("Started");
            //ResetActionList();
            //ResetGoalList();
        }

        protected virtual void AddAction(CActionBase action)
        {
            if (!this.actionList.Contains(action))
            {
                this.actionList.Add(action);
            }
        }
        protected virtual void AddGoal(CGoal goal)
        {
            if (!this.goalList.Contains(goal))
            {
                this.goalList.Add(goal);
            }
        }

        protected virtual void ResetActionList()
        {
            actionList = new List<CActionBase>();
            CActionBase[] acts = this.GetComponents<CActionBase>();
            foreach (CActionBase a in acts)
            {
                actionList.Add(a);
            }
        }

        protected virtual void ResetGoalList()
        {
            goalList = new List<CGoal>();
            CGoal[] gos = this.GetComponents<CGoal>();
            foreach (CGoal g in gos)
            {
                goalList.Add(g);
            }
            goalBlacklist = new List<CGoal>();
        }

        // Called after t time to reset blacklist back to goal list
        protected virtual void ResetBlackList()
        {
            foreach (CGoal g in goalBlacklist)
            {
                if (!goalList.Contains(g))
                {
                    goalList.Add(g);
                }
                goalBlacklist.Remove(g);
            }
        }

        // Function to create a new plan (action queue)
        protected virtual void GetAGoal()
        {
            planner = new CPlanner();
            currentGoal = null;
            var sortedGoal = goalList.OrderBy(g => g.important);
            foreach (CGoal g in sortedGoal)
            {
                actionQueue = planner.Plan(g, actionList, agentFact);
                if (actionQueue != null)
                {
                    currentGoal = g;
                    break;
                }
            }
            Debug.Log(currentGoal);
        }

        protected virtual void AskForInterupt() { }

        protected virtual void LateUpdate()
        {
            // Check if currently running any action
            if ((currentAction != null) && (currentAction.isActive))
            {
                // If sometime when running, player or user ask to stop the action. stop and re-plan
                if ((interupt) && (currentAction.isInteruptable))
                {
                    currentAction = null;
                    GetAGoal();
                }
                // Check if the current action has complete yet?
                if (currentAction.isComplete)
                {
                    // Do pos calculation and switch to the next action
                    currentAction.Pos_Perform();
                    currentAction.isActive = false;
                    if (currentAction.forceReplan == true)
                    {
                        GetAGoal();
                        return;
                    }
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
                        // If goal is satisfied and non repeat, remove from the goal list
                        if (currentGoal.deletable)
                        {
                            goalList.Remove(currentGoal);
                        }
                        // And then find a new goal
                        GetAGoal();
                    }

                    // Take 1 action from the plan queue, and execute it.
                    currentAction = actionQueue.Dequeue();
                    Debug.Log("Currently performing: " + currentAction.actionName);
                    // If the action is performable by checking Pre_performing calculation, default always return true
                    if (currentAction.Pre_Perform())
                    {
                        currentAction.isActive = true;
                        // If durring perfoming action, things happen that cause the action to fail, temporary remove 
                        // the goal and re-plan.
                        if (currentAction.PerformAction())
                        {
                            goalList.Remove(currentGoal);
                            goalBlacklist.Add(currentGoal);
                            GetAGoal();
                        }
                    }
                    // If checking Pre_performing false, meaning the action is unable to perform for some reason, temporary remove 
                    // the goal and re-plan.
                    else
                    {
                        goalList.Remove(currentGoal);
                        goalBlacklist.Add(currentGoal);
                        GetAGoal();
                    }
                }
                else
                {
                    GetAGoal();
                }
            }
        }
    }

}
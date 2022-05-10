using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using World;
using Action;
using Goal;
using Planner;

public class CAgent : MonoBehaviour
{
    public string agentName = "agent Name";

    public List<CFact> belief;

    public List<CActionBase> actionList;
    public List<CGoal> goalList;
    protected List<CActionBase> possibleAction;
    public List<CGoal> goalBlacklist;
    protected Queue<CActionBase> actionQueue;
    protected CActionBase currentAction;
    protected CGoal currentGoal;

    protected CPlanner planner;

    protected virtual void Awake()
    {
        //ResetActionList();
        //ResetGoalList();
    }

    protected virtual void Start()
    {
        ResetActionList();
        ResetGoalList();
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
    protected virtual void MakeNewPlan()
    {
        planner = new CPlanner();
        var sortedGoal = goalList.OrderBy(g => g.important);
        //foreach (CGoal g in sortedGoal)
        //{
        //    actionQueue = planner.Plan();
        //    if (actionQueue != null)
        //    {
        //        currentGoal = g;
        //    }
        //}
        while (currentGoal != null)
        {
            CGoal g = sortedGoal.FirstOrDefault();
            if (g != null)
            {
                actionQueue = planner.Plan(g, actionList, belief);

                if (actionQueue != null) {
                    currentGoal = g;
                }
            }
        }
    }

    protected virtual void LateUpdate()
    {
        // Check if currently running any action
        if ((currentAction != null) && (currentAction.isActive))
        {
            // Check if the current action has complete yet?
            if (currentAction.isComplete)
            {
                // Do pos calculation and switch to the next action
                currentAction.Pos_Perform();
                currentAction.isActive = false;
            }
        }
        else
        {
            // If there is a goal to pursuit
            if (currentGoal != null)
            {
                // Check if goal is satified
                if (currentGoal.IsSatified())
                {
                    // If goal is satisfied and non repeat, remove from the goal list
                    if (currentGoal.deletable)
                    {
                        goalList.Remove(currentGoal);
                    }
                    // And then find a new goal
                    MakeNewPlan();
                }

                // Check if done all action is finished but not satisfied, this mean the goal must not been able to complete
                // If this happen, temporary remove to the blacklist, so that after T time, be put back to the list.
                if (actionQueue.Count <= 0)
                {
                    goalList.Remove(currentGoal);
                    goalBlacklist.Add(currentGoal);
                    MakeNewPlan();
                }

                // Take 1 action from the plan queue, and execute it.
                currentAction = actionQueue.Dequeue();
                Debug.Log("Currently performing: " + currentAction.actionName);
                if (currentAction.Pre_Perform())
                {
                    currentAction.isActive = true;
                    currentAction.PerformAction();
                }
            }
        }
    }
}

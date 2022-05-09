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

        }
        else
        {
            // If there is a goal to pursuit
            if (currentGoal != null)
            {
                // Check if goal is satified
                if (currentGoal.IsSatified())
                {
                    if (currentGoal.deletable)
                    {
                        goalList.Remove(currentGoal);
                    }
                    MakeNewPlan();
                }

                // Check if done all action is finished but not satisfied
                if (actionQueue.Count <= 0)
                {
                    goalList.Remove(currentGoal);
                    goalBlacklist.Add(currentGoal);
                    MakeNewPlan();
                }

                // Take 1 action from the plan queue, and execute it.
                currentAction = actionQueue.Dequeue();
            }
        }
    }
}

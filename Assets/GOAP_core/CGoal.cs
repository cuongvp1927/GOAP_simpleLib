using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.World;
using Unity.GOAP.Agent;

namespace Unity.GOAP.Goal
{
    public class CGoal : Node
    {
        public int important = 1;
        public string goalName = "SomeGoal";
        public bool deletable = true;
        
        public List<CFact> goalList;

        public CFactManager goals;

        public CGoal() : base()
        {
            this.goalName = this.GetType().Name;
        }
        
        public virtual void Initiate()
        {
            goals = new CFactManager(goalList);
        }

        // Function called on starting the goal
        public virtual void OnStart(CAgent agent)
        {
            Debug.Log("Agent: " + agent.agentName + " start for goal: " + this.goalName);
            return;
        }

        // Function called on completing the goal
        public virtual void OnComplete(CAgent agent)
        {
            Debug.Log("Agent: " + agent.agentName + " complete Goal: " + this.goalName);
            return;
        }

        // Function to check if goal is satisfied
        public virtual bool IsSatified(CFactManager curState)
        {
            //if (curState.CompareFactList(goals))
            if (goals.CompareFactList(curState))
                {
                    return true;
            }
            return false;
        }

    }
}

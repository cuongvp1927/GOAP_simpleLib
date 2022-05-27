using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.World;

namespace Unity.GOAP.Goal
{
    public class CGoal : Node
    {
        public int important = 1;
        public string goalName = "SomeGoal";
        [HideInInspector]public bool deletable = true;
        
        public List<CFact> goalList;

        public CFactManager goals;
        public bool finished = false;

        protected virtual void Start()
        {
            goals = new CFactManager(goalList);
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

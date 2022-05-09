using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using World;

namespace Goal
{
    public class CGoal : MonoBehaviour
    {
        public int important = 1;
        public string goalName = "SomeGoal";
        public bool deletable = true;
        
        public List<CFact> goals;


        public void Awake()
        {
            goals = new List<CFact>();
        }

        public CGoal() {}

        public CGoal(string n, int i, bool d)
        {
            this.name = n;
            this.important = i;
            this.deletable = d;
        }

        public bool IsSatified()
        {
            foreach (CFact goal in goals)
            {
                CFact vp = CWorld.Instance.HasFact(goal.name);
                if ((vp==null) || (!vp.Equals(goal)))
                {
                    return false;
                }
            }
            return true;
        }

    }
}

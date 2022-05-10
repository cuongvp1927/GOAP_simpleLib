using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Action;
using Goal;
using World;

namespace Planner
{
    class Node
    {
        Node parent;
        CActionBase action;
    }

    public class CPlanner
    {
        public Queue<CActionBase> Plan(CGoal goal, List<CActionBase> actionList, List<CFact> belief)
        {
            return null;
        }
    }
}


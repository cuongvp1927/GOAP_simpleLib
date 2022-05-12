//using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using System.Linq;

using Action;
using Goal;
using World;

namespace Planner
{
    class Node
    {
        public Node parent;
        public CActionBase action;
        public int cost;
        public List<CFact> currentState;

        public Node (Node parent, int cost, CActionBase action, List<CFact> states)
        {
            this.parent = parent;
            this.cost = cost;
            this.action = action;
            currentState = new List<CFact>(states);
        }
    }

    public class CPlanner
    {
        public CPlanner() {}

        #region UtilityFunction
        List<CActionBase> GetDoableActions(List<CFact> curState, List<CActionBase> actionList)
        {
            List<CActionBase> doableAction = new List<CActionBase>();

            foreach (CActionBase act in actionList)
            {
                bool doable = true;
                foreach (CFact fact in act.pre_conditions)
                {
                    if (!curState.Contains(fact))
                    {
                        doable = false;
                        break;
                    }
                }
                if (doable) 
                    doableAction.Add(act);
            }

            return doableAction;
        }

        #endregion

        // This implementation is long and costly
        // If have time, improve it with State table and better search algorithm for better performance
        public Queue<CActionBase> Plan(CGoal goal, List<CActionBase> actionList, List<CFact> agentState)
        {
            
            List<Node> leaves = new List<Node>();
            // The first node have no parent, no action, no cost, and take the current world state as current state
            Node startNode = new Node(null, 0, null, CWorld.Instance.GetFacts());
            // Find a plan
            bool hasPlan = FindPlan(startNode, leaves, goal, actionList);

            // If do not found a plan
            if (!hasPlan)
            {
                return null;
            }

            // Get the cheapest plan
            Node cheapestLeaf = leaves[0];
            foreach (Node l in leaves)
            {
                if (l.cost < cheapestLeaf.cost)
                {
                    cheapestLeaf = l;
                }
            }

            // From the cheapest leaf, trace back to the root, with each action added to the action queue
            Queue<CActionBase> actionQueue = new Queue<CActionBase>();
            while (cheapestLeaf != null)
            {
                if (cheapestLeaf.action !=null)
                    actionQueue.Enqueue(cheapestLeaf.action);

                cheapestLeaf = cheapestLeaf.parent;
            }

            Queue<CActionBase> re = new Queue<CActionBase>(actionQueue.Reverse());

            return re;
        }

        public Queue<CActionBase> PlanAStar(CGoal goal)
        {
            return null;
        }

        // Currently, this method find all possible combination of action sequence, with piority given by the cost of each action
        bool FindPlan(Node parent, List<Node> leaves, CGoal goal, List<CActionBase> actionList) 
        {
            bool foundpath = false;
            // Check doable action
            List<CActionBase> aList = GetDoableActions(parent.currentState, actionList);
            var sortedActions = aList.OrderBy(a => a.cost);

            // Take out action in order of cost
            foreach (CActionBase act in sortedActions)
            {
                // Add effect of action to current state of node
                List<CFact> states = new List<CFact>(parent.currentState);
                foreach (CFact f in act.effects)
                {
                    if (!states.Contains(f))
                    {
                        states.Add(f);
                    }
                }
                // Create new node as the next node of graph
                Node child = new Node(parent, parent.cost + act.cost, act, states);

                // If the goal is complete by this action, this action is a leave, and a plan is found
                if (goal.IsSatified(states))
                {
                    leaves.Add(child);
                    foundpath = true;
                }
                // Else, remove this action from the action list, and move on to the next loop
                else
                {
                    actionList.Remove(act);
                    foundpath = FindPlan(child, leaves, goal, actionList);
                }
            }

            return foundpath;
        }

        void BuildGraphAStar()
        {

        }
    }
}


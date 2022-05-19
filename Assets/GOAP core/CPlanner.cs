using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Unity.GOAP.Action;
using Unity.GOAP.Goal;
using Unity.GOAP.World;

namespace Unity.GOAP.Planner
{
    class Node
    {
        public Node parent;
        public CActionBase action;
        public int cost;
        public CFactManager currentState;

        public Node (Node parent, int cost, CActionBase action, List<CFact> states)
        {
            this.parent = parent;
            this.cost = cost;
            this.action = action;
            currentState = new CFactManager(states);
        }
    }

    public class CPlanner
    {
        public CPlanner() {}

        #region UtilityFunction
        List<CActionBase> GetDoableActions(CFactManager curState, List<CActionBase> actionList)
        {
            List<CActionBase> doableAction = new List<CActionBase>();

            foreach (CActionBase act in actionList)
            {
                bool doable = true;

                foreach (CFact fact in act.preconditions.GetFactList())
                {
                    if (!curState.HasFact(fact))
                    {
                        doable = false;
                        break;
                    }
                }
                if (doable) 
                    doableAction.Add(act);
            }

            Debug.Log("Current state: ");
            foreach (CFact f in curState.GetFactList())
            {
                Debug.Log(f.name);
            }

            Debug.Log("Doable Action: ");
            foreach (CActionBase a in doableAction)
            {
                Debug.Log(a.actionName);
            }

            return doableAction;
        }

        #endregion

        // This implementation is long and costly
        // If have time, improve it with State table and better search algorithm for better performance
        public Queue<CActionBase> Plan(CGoal goal, List<CActionBase> actionList, CFactManager agentFact)
        {
            List<Node> leaves = new List<Node>();
            // The first node have no parent, no action, no cost, and take the current world state and agent states as current state
            CFactManager listFact = new CFactManager(CWorld.Instance.GetFacts().GetFactList());

            foreach (CFact f in agentFact.GetFactList())
            {
                if (!listFact.HasFact(f))
                {
                    listFact.AddFact(f.name, f.value);
                }
            }

            Node startNode = new Node(null, 0, null, listFact.GetFactList());
            // Find a plan
            bool hasPlan = FindPlan(startNode, leaves, goal, actionList);

            // If do not found a plan
            if (!hasPlan)
            {
                Debug.Log("No plan found");
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
                if (cheapestLeaf.action != null)
                    actionQueue.Enqueue(cheapestLeaf.action);

                cheapestLeaf = cheapestLeaf.parent;
            }

            Queue<CActionBase> re = new Queue<CActionBase>(actionQueue.Reverse());

            Debug.Log("Action queue found");
            foreach (CActionBase a in re)
            {
                Debug.Log(a.actionName);
            }

            return re;
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
                CFactManager states = new CFactManager(parent.currentState.GetFactList());
                foreach (CFact f in act.effects.GetFactList())
                {
                    if (!states.HasFact(f))
                    {
                        states.AddFact(f.name, f.value);
                    }
                }
                // Create new node as the next node of graph
                Node child = new Node(parent, parent.cost + act.cost, act, states.GetFactList());

                // If the goal is complete by this action, this action is a leaf, and a plan is found
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

    }
}


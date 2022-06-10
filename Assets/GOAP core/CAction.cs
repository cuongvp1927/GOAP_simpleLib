using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.World;
using Unity.GOAP.Agent;
namespace Unity.GOAP.ActionBase {
    public class CActionBase : Node
    {
        public int cost = 1;
        public string actionName = "New Action";
        //public float duration = 0.5f;
        //public float preActionDuration = 0.5f;
        //public float posActionDuration = 0.5f;

        public List<CFact> PreConditions;

        // The special effect used for planning only, does not affect the world state and the agent's state
        public List<CFact> Effects;

        // The effect to the agent's state, does not affect the world state
        public List<CFact> agentEffects;

        // The effect to the world state, does not affect the agent's state
        public List<CFact> worldEffects;

        [HideInInspector] protected CAgent agent;

        [HideInInspector] public bool isActive = false;
        [HideInInspector] public bool isInteruptable = false;
        [HideInInspector] public bool forceReplan = false;

        [HideInInspector] public CFactManager preconditions;
        [HideInInspector] public CFactManager effects;

        public virtual void Initiate()
        {
            preconditions = new CFactManager();
            effects = new CFactManager();
            
            foreach (CFact f in PreConditions)
            {
                preconditions.AddFact(f.name, f.value);
            }

            foreach (CFact f2 in Effects)
            {
                effects.AddFact(f2.name, f2.value);
            }
            foreach (CFact f2 in agentEffects)
            {
                effects.AddFact(f2.name, f2.value);
            }
            foreach (CFact f2 in worldEffects)
            {
                effects.AddFact(f2.name, f2.value);
            }
        }

        // Check complete, required
        public virtual bool HasCompleted(CAgent agent)
        {
            return true;
        }
        // Check if failed, required
        public virtual bool HasFailed(CAgent agent)
        {
            return false;
        }

        // Main action, required
        public virtual bool PerformAction(CAgent agent)
        {
            return false;
        }
        // Pre calculation if needed, return true to start performing the action.
        public virtual bool Pre_Perform(CAgent agent)
        {
            return true;
        }
        // Pos calculation if needed.
        public virtual bool Pos_Perform(CAgent agent)
        {

            Debug.Log("Complete performing: " + actionName);
            return true;
        }

        [HideInInspector]public List<Node> childiren = new List<Node>();
    }
}

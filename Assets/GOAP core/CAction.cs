using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.World;
using Unity.GOAP.Agent;

namespace Unity.GOAP.Action {
    [System.Serializable]
    public abstract class CActionBase: MonoBehaviour
    {
        public int cost = 1;
        public string actionName = "New Action";
        //public float duration = 0.5f;
        //public float preActionDuration = 0.5f;
        //public float posActionDuration = 0.5f;

        public List<CFact> PreConditions;
        public List<CFact> Effects;

        CAgent agent;

        public bool isActive = false;
        public bool isInteruptable = false;
        public bool forceReplan = false;

        public bool isComplete;

        public CFactManager preconditions;
        public CFactManager effects;

        public virtual void Awake()
        {
            PreConditions = new List<CFact>();
            Effects = new List<CFact>();

            isComplete = false;
        }

        public virtual void Start()
        {
            preconditions = new CFactManager(PreConditions);
            effects = new CFactManager(Effects);
        }

        // Main action, required
        public abstract bool PerformAction();
        // Pre calculation if needed, return true to start performing the action.
        public virtual bool Pre_Perform()
        {
            return true;
        }
        // Pos calculation if needed.
        public virtual bool Pos_Perform()
        {
            return true;
        }
    }
}

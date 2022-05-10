using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using World;
namespace Action {
    public abstract class CActionBase: MonoBehaviour
    {
        public int cost = 1;
        public string actionName = "New Action";
        public float duration = 0.5f;
         
        public List<CFact> pre_conditions;
        public List<CFact> effects;

        CAgent agent;

        public bool isActive = false;
        public bool isInteruptable = false;
        public bool forceReplan = false;

        public bool isComplete;
        public bool isFail;

        public void Awake()
        {
            pre_conditions = new List<CFact>();
            effects = new List<CFact>();

            isComplete = false;
            isFail = false;
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
            isFail = false;
            return true;
        }
    }
}

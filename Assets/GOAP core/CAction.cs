using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using World;
namespace ActionBase {
    public class CActionBase : Node
    {
        public int cost = 1;
        public string actionName = "New Action";
        public float duration = 0.5f;
         
        public List<CFact> pre_conditions;
        public List<CFact> effects;

        CAgent agent;

        [HideInInspector]public bool isActive = false;
        [HideInInspector]public bool isInteruptable = false;
        [HideInInspector]public bool forceReplan = false;

        [HideInInspector]public bool isComplete;
        [HideInInspector]public bool isFail;

        public void Awake()
        {
            pre_conditions = new List<CFact>();
            effects = new List<CFact>();

            isComplete = false;
            isFail = false;
        }
        
        // Main action, required
        public virtual bool PerformAction()
        {
            return false;
        }
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

        [HideInInspector]public List<Node> childiren = new List<Node>();
    }
}

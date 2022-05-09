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

        public void Awake()
        {
            pre_conditions = new List<CFact>();
            effects = new List<CFact>();
        }

        public abstract void PerformAction();
        public abstract void Pre_Perform();
        public abstract void Pos_Perform();
    }
}

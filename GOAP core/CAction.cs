using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using World;
namespace Action {
    public abstract class CActionBase
    {
        public int cost = 1;
        public string name = "New Action";
        public float duration = 0.5f;

        public List<CFact> pre_conditions;
        public List<CFact> effects;

        bool isActive = false;
        bool isInteruptable = false;
        bool forceReplan = false;


        public abstract void PerformAction();
        public abstract void Pre_Perform();
        public abstract void Pos_Perform();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.World;
using Unity.GOAP.Action;

public class ActionGoToWaitingArea : CActionBase
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
    }

    public override bool PerformAction()
    {
        return true;
    }


}

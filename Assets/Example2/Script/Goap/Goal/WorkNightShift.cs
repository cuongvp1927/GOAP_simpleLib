using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class WorkNightShift : CGoal
{
    IAgentExp2 worker;
    public override void Initiate(CAgent a)
    {
        base.Initiate(a);
        worker = (IAgentExp2)a;
    }

    public override void OnComplete()
    {
        base.OnComplete();
        worker.EarnMoney(50);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class Eat : CGoal
{
    IAgentExp2 worker;
    public override void Initiate(CAgent a)
    {
        worker = (IAgentExp2)a;
        base.Initiate(a);
    }

    public override void OnComplete()
    {
        base.OnComplete();
        worker.ResetHunger();
    }
}


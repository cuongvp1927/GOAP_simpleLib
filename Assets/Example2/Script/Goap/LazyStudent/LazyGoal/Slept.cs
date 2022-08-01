using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;
public class Slept : CGoal
{
    IAgentExp2 student;
    public override void Initiate(CAgent a)
    {
        base.Initiate(a);
        student = (IAgentExp2)a;
    }

    public override void OnStart()
    {
        base.OnStart();
    }
    public override void OnComplete()
    {
        base.OnComplete();
        student.ResetBoredom();
    }
}

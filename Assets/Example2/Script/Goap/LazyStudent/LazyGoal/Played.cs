using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class Played : CGoal
{
    LazyStudentSecVer student;
    public override void Initiate(CAgent a)
    {
        base.Initiate(a);
        student = (LazyStudentSecVer)a;
    }

    public override void OnStart()
    {
        base.OnStart();
        Debug.Log(this.important);
    }
    public override void OnComplete()
    {
        base.OnComplete();
    }
}

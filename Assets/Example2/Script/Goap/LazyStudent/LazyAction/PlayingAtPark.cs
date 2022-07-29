using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class PlayingAtPark : CActionBase
{
    float timer = 0;
    [SerializeField] float playTime = 5f;
    LazyStudentSecVer student;

    public override void Initiate(CAgent a)
    {
        base.Initiate(a);
        student = (LazyStudentSecVer)agent;
    }
    public override bool Pre_Perform()
    {
        timer = 0;
        if (student.agentFact.GetFact("FreeTime").value != 1)
        {
            student.IncreaseSkipCounter();
        }

        return true;
    }

    public override bool PerformAction()
    {
        isActive = true;
        return true;
    }

    public override bool Pos_Perform()
    {
        return base.Pos_Perform();
    }
    public override bool HasCompleted()
    {
        timer += Time.deltaTime;
        if (timer >= playTime)
        {
            return true;
        }
        return false;
    }

    public override bool HasFailed()
    {
        return false;
    }

}

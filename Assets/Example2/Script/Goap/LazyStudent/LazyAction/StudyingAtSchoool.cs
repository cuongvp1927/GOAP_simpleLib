using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class StudyingAtSchoool : CActionBase
{

    public override bool Pre_Perform()
    {
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

        if (agent.agentFact.GetFact("FreeTime").value ==1)
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

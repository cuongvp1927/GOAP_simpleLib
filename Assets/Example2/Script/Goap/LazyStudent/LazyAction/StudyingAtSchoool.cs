using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class StudyingAtSchoool : CActionBase
{

    public override bool Pre_Perform(CAgent agent)
    {
        return true;
    }

    public override bool PerformAction(CAgent agent)
    {
        isActive = true;
        return true;
    }

    public override bool Pos_Perform(CAgent agent)
    {
        return base.Pos_Perform(agent);
    }
    public override bool HasCompleted(CAgent agent)
    {

        if (agent.agentFact.GetFact("FreeTime").value ==1)
        {
            return true;
        }

        return false;
    }

    public override bool HasFailed(CAgent agent)
    {
        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
public class Sleeping : CActionBase
{
    float timer = 0f;
    public override bool Pre_Perform(CAgent agent)
    {
        timer = 0f;
        return base.Pre_Perform(agent);
    }

    public override bool PerformAction(CAgent agent)
    {
        return base.PerformAction(agent);
    }

    public override bool Pos_Perform(CAgent agent)
    {
        agent.agentFact.RemoveFact("finishedWorking");
        return base.Pos_Perform(agent);
    }
    public override bool HasCompleted(CAgent agent)
    {
        timer += Time.deltaTime;
        if (timer >=5f)
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

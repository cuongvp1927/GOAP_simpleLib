using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class WorkingExtra : CActionBase
{
    float timer = 0f;
    [SerializeField] float workTime = 10f;
    public WorkingExtra() : base()
    {
        this.actionName = "WorkingExtra";
    }
    public override bool Pre_Perform(CAgent agent)
    {
        return base.Pre_Perform(agent);
    }

    public override bool PerformAction(CAgent agent)
    {
        return base.PerformAction(agent);
    }

    public override bool Pos_Perform(CAgent agent)
    {
        agent.agentFact.ChangeFact("needMoney", 0);
        return base.Pos_Perform(agent);
    }
    public override bool HasCompleted(CAgent agent)
    {
        timer += Time.deltaTime;
        if (timer >= workTime)
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
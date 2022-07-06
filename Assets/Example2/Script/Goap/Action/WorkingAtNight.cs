using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class WorkingAtNight : CActionBase
{
    public WorkingAtNight() : base()
    {
        this.actionName = "WorkingAtNight";
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
        return base.Pos_Perform(agent);
    }
    public override bool HasCompleted(CAgent agent)
    {
        if (CWorld.Instance.GetFacts().GetFact("nightShift").value != 1)
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
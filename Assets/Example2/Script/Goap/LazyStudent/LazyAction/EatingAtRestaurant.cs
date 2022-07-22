using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class EatingAtRestaurant : CActionBase
{
    float timer = 0f;
    [SerializeField] float eatTime = 1f;
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
        timer += Time.deltaTime;
        if (agent.agentFact.GetFact("EatTime").value !=1 || timer < eatTime)
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

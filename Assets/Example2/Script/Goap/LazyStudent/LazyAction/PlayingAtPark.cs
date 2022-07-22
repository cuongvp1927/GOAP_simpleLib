using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class PlayingAtPark : CActionBase
{
    float timer = 0;
    [SerializeField] float playTime = 5f;
    public override bool Pre_Perform(CAgent agent)
    {
        timer = 0;
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
        if (timer < playTime)
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

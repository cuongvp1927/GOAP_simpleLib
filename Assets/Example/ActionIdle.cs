using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
public class ActionIdle : CActionBase
{

    public override bool Pre_Perform(CAgent agent)
    {
        return true;
    }

    float timer = 0f;
    public override bool Pos_Perform(CAgent agent)
    {
        timer = timer + Time.deltaTime;
        if (timer >= 1f)
        {
            Debug.Log("Complete performing: " + actionName);
            this.isActive = false;
            timer = 0;
        }

        return true;
    }
    public override bool PerformAction(CAgent agent)
    {
        isActive = true;
        return true;
    }

    public override bool HasCompleted(CAgent agent)
    {
        return true;
    }

    public override bool HasFailed(CAgent agent)
    {
        return false;
    }


}

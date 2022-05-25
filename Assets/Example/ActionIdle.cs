using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Unity.GOAP.Action;
using Unity.GOAP.Agent;
public class ActionIdle : CActionBase
{
    public override void Awake()
    {
        base.Awake();
    }

    public override bool Pre_Perform()
    {
        return true;
    }

    float timer = 0f;
    public override bool Pos_Perform()
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
    public override bool PerformAction()
    {
        isActive = true;
        return true;
    }

    public override bool HasCompleted()
    {
        return true;
    }

    public override bool HasFailed()
    {
        return false;
    }


}

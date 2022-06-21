using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;

public class ActionGoToRegister : CActionBase
{
    public override bool Pre_Perform(CAgent agent)
    {
        GameObject target = GameObject.FindWithTag("Reception");
        if (target == null)
        {
            return false;
        }
        agent.position3D = target.transform.position;
        return true;
    }

    public override bool PerformAction(CAgent agent)
    {
        Patient patient = (Patient)agent;
        patient.Move();
        isActive = true;

        return true;
    }

    public override bool HasCompleted(CAgent agent)
    {
        Patient patient = (Patient)agent;
        if (patient.navAgent.remainingDistance < 2f)
        {
            return true;
        }
        return false;
    }

    public override bool HasFailed(CAgent agent)
    {
        Patient patient = (Patient)agent;
        if (HasCompleted(agent))
        {
            return false;
        }

        if (patient.navAgent.enabled && !patient.navAgent.hasPath && !patient.navAgent.pathPending && patient.navAgent.remainingDistance == 0)
        {
            return true;
        }
        return false;
    }

    float timer = 0f;
    public override bool Pos_Perform(CAgent agent)
    {
        timer = timer + Time.deltaTime;
        this.isActive = true;
        if (timer >= 2f)
        {
            Debug.Log("Complete performing: " + actionName);
            this.isActive = false;
            timer = 0;
        }

        return true;
    }
}

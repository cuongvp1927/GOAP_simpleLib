using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;

public class ActionGoHome : CActionBase
{
    public override bool Pre_Perform(CAgent agent)
    {
        GameObject target = GameObject.FindGameObjectWithTag("Home");
        if (target == null)
        {
            return false;
        }

        agent.position3D = target.transform.position;
        return true;
    }

    public override bool Pos_Perform(CAgent agent)
    {
        Debug.Log("Complete performing: " + actionName);
        isActive = false;
        //Destroy(this.gameObject);
        return true;
    }

    public override bool PerformAction(CAgent agent)
    {
        Patient patient = (Patient)agent;
        patient.navAgent.SetDestination(agent.position3D);
        isActive = true;
        return true;
    }

    public override bool HasCompleted(CAgent agent)
    {
        Patient patient = (Patient)agent;
        if (patient.navAgent.remainingDistance < 2f)
            return true;
        return false;
    }

    public override bool HasFailed(CAgent agent)
    {
        if (HasCompleted(agent))
        {
            return false;
        }
        Patient patient = (Patient)agent;
        if (patient.navAgent.enabled && !patient.navAgent.hasPath && !patient.navAgent.pathPending && patient.navAgent.remainingDistance == 0)
        {
            return true;
        }
        return false;
    }
}

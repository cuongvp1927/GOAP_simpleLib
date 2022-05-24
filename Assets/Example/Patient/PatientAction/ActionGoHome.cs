using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Unity.GOAP.Action;
using Unity.GOAP.Agent;

[RequireComponent(typeof(NavMeshAgent))]
public class ActionGoHome : CActionBase
{
    NavMeshAgent navAgent;
    public override void Awake()
    {
        base.Awake();

        this.navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        this.agent = this.gameObject.GetComponent<CAgent>();
    }
    public override bool Pre_Perform()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Home");
        if (target == null)
        {
            return false;
        }

        agent.position3D = target.transform.position;
        return true;
    }

    public override bool Pos_Perform()
    {
        //Destroy(this.gameObject);
        isActive = false;
        return true;
    }

    public override bool PerformAction()
    {
        navAgent.SetDestination(agent.position3D);
        isActive = true;
        return true;
    }

    public override bool HasCompleted()
    {
        if (navAgent.remainingDistance < 2f)
            return true;
        return false;
    }

    public override bool HasFailed()
    {
        if (HasCompleted())
        {
            return false;
        }

        if (navAgent.enabled && !navAgent.hasPath && !navAgent.pathPending && navAgent.remainingDistance == 0)
        {
            return true;
        }
        return false;
    }
}

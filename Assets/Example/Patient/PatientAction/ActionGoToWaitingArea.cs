using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Unity.GOAP.Agent;
using Unity.GOAP.Action;
using Unity.GOAP.World;

[RequireComponent(typeof(NavMeshAgent))]
public class ActionGoToWaitingArea : CActionBase
{
    // Start is called before the first frame update
    NavMeshAgent navAgent;

    public override void Awake()
    {
        base.Awake();

        this.navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        this.agent = this.gameObject.GetComponent<CAgent>();
    }

    public override bool Pre_Perform()
    {
        GameObject target;
        target = GameObject.FindWithTag("WaitingArea");
        if (target == null)
        {
            return false;
        }
        agent.position3D = target.transform.position;

        return true;
    }

    public override bool PerformAction()
    {
        navAgent.SetDestination(agent.position3D);
        isActive = true;

        return true;
    }

    public override bool Pos_Perform()
    {
        ResourceManager.Instance.AddPatient(agent);

        isActive = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Unity.GOAP.Agent;
using Unity.GOAP.Action;

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

        return base.Pre_Perform();
    }

    public override bool PerformAction()
    {
        navAgent.SetDestination(agent.position3D);
        return true;
    }

    public override bool IsComplete()
    {
        if (navAgent.remainingDistance < 2f)
            return true;
        return false;
    }

}

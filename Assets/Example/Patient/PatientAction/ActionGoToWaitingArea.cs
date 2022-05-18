using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Unity.GOAP.World;
using Unity.GOAP.Action;

[RequireComponent(typeof(NavMeshAgent))]
public class ActionGoToWaitingArea : CActionBase
{
    // Start is called before the first frame update
    NavMeshAgent navAgent;
    GameObject target;

    public override void Awake()
    {
        base.Awake();

        this.navAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    public override bool Pre_Perform()
    {
        target = GameObject.FindWithTag("WaitingArea");
        if (target == null)
        {
            return false;
        }

        return base.Pre_Perform();
    }

    public override bool PerformAction()
    {
        navAgent.SetDestination(target.transform.position);
        return true;
    }


}

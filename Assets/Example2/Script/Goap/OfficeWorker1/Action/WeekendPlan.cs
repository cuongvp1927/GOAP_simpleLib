using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using UnityEngine.AI;

public class WeekendPlan : CActionBase
{
    private NavMeshAgent _navMeshAgent;
    float timer;
    public override bool Pre_Perform(CAgent agent)
    {
        timer = 0.0f;
        AllLocationInfor infor = AllLocationInfor.Instance;
        LocationInformation loc = infor.infos.Find(e => e.codeName.Equals("Park3"));

        if (loc != null)
        {
            agent.position3D = loc.obj.transform.position;
            return true;
        }

        Debug.LogError("Can not find position of the park");
        return false;
    }

    public override bool PerformAction(CAgent agent)
    {
        _navMeshAgent = agent.gameObject.GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(agent.position3D);
        isActive = true;
        return true;
    }

    public override bool Pos_Perform(CAgent agent)
    {

        isActive = true;
        timer += Time.deltaTime;
        if (timer >= 10f)
        {
            Debug.Log("Complete performing: " + actionName);
            isActive = false;
        }
        return true;
    }
    public override bool HasCompleted(CAgent agent)
    {
        if (_navMeshAgent.pathPending)
        {
            return false;
        }

        if ((_navMeshAgent.remainingDistance <= 1f))
        {

            return true;
        }

        return false;
    }

    public override bool HasFailed(CAgent agent)
    {

        if (HasCompleted(agent))
        {
            return false;
        }

        if (_navMeshAgent.enabled && !_navMeshAgent.hasPath && !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance == 0)
        {
            return true;
        }
        return false;
    }

}

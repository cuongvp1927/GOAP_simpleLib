using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.ActionBase;
using Unity.GOAP.World;
using Unity.GOAP.Agent;
using UnityEngine.AI;

public class GoToWork : CActionBase
{
    private NavMeshAgent _navMeshAgent;
    public override bool Pre_Perform(CAgent agent)
    {

        AllLocationInfor infor = AllLocationInfor.Instance;
        LocationInformation loc = infor.infos.Find(e => e.codeName.Equals("PostOffice"));

        if (loc != null)
        {
            agent.position3D = loc.obj.transform.position;
            return true;
        }

        Debug.LogError("Can not find position of post office");
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
        this.isActive = true;
        if (CWorld.Instance.GetFacts().GetFact("relaxingTime").value == 1)
        {
            Debug.Log("Complete performing: " + actionName);
            this.isActive = false;
        }
        return true;
    }
    public override bool HasCompleted(CAgent agent)
    {
        if (_navMeshAgent.pathPending)
        {
            return false;
        }

        if (_navMeshAgent.remainingDistance <= 2f)
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

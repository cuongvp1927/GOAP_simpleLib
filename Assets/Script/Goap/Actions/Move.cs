using System.Collections;
using System.Collections.Generic;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using UnityEngine;
using UnityEngine.AI;

public class Move : CActionBase
{
    [SerializeField] private string location;
    private NavMeshAgent _navMeshAgent;
    public override bool Pre_Perform(CAgent agent)
    {
        AllLocationInfor infor = AllLocationInfor.Instance;
        LocationInformation loc = infor.infos.Find(e => e.codeName.Equals(location));
        if (loc != null)
        {
            agent.position3D = loc.obj.transform.position;
            return true;
        }

        Debug.LogError("Can not find position of school");
        return false;
    }

    public override bool PerformAction(CAgent agent)
    {
        _navMeshAgent = agent.gameObject.GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(agent.position3D);
        isActive = true;
        return true;
    }

    public override bool HasCompleted(CAgent agent)
    {
        if (_navMeshAgent.remainingDistance <= 1f)
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

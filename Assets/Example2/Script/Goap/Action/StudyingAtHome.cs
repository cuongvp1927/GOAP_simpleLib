using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using UnityEngine.AI;
using Unity.GOAP.World;

    public class StudyingAtHome : CActionBase
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] float studyTime = 2f;
    float timer;
    public StudyingAtHome() : base()
    {
        this.actionName = "StudyingAtHome";
    }
    public override bool Pre_Perform(CAgent agent)
    {
        timer = 0;
        AllLocationInfor infor = AllLocationInfor.Instance;
        LocationInformation loc = infor.infos.Find(e => e.codeName.Equals("Home1"));


        if (loc != null)
        {
            agent.position3D = loc.obj.transform.position;
            agent.agentFact.RemoveContains("atLoc");

            return true;
        }

        Debug.LogError("Can not find position of the location home1");
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
        if (timer >= studyTime)
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Unity.GOAP.Action;
using Unity.GOAP.Agent;

[RequireComponent(typeof(NavMeshAgent))]
public class ActionGetPatient : CActionBase
{
    NavMeshAgent navAgent;
    Patient patient;
    GameObject cube;

    public override void Awake()
    {
        base.Awake();

        this.navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        this.agent = this.gameObject.GetComponent<CAgent>();
    }
    public override bool Pre_Perform()
    {
        cube = ResourceManager.Instance.RemoveCube();
        if (cube == null)
        {
            return false;
        }

        patient = (Patient) ResourceManager.Instance.RemovePatient();
        if (patient == null)
        {
            ResourceManager.Instance.AddCube(cube);
            return false;
        }

        agent.position3D = patient.transform.position;
        return true;
    }

    public override bool Pos_Perform()
    {
        patient.inventory.Add(cube);

        Nurse nurse = (Nurse)agent;
        nurse.inventory.Add(cube);

        return base.Pos_Perform();
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

    public override bool PerformAction()
    {
        navAgent.SetDestination(agent.position3D);
        isActive = true;

        return true;
    }

}

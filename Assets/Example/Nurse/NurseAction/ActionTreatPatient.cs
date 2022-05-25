using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Unity.GOAP.Action;
using Unity.GOAP.Agent;

[RequireComponent(typeof(NavMeshAgent))]
public class ActionTreatPatient : CActionBase
{
    NavMeshAgent navAgent;

    GameObject cube;
    public override void Awake()
    {
        base.Awake();

        this.navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        this.agent = this.gameObject.GetComponent<CAgent>();
    }

    public override bool Pre_Perform()
    {
        Nurse nurse = (Nurse)agent;
        foreach (GameObject go in nurse.inventory)
        {
            if (go.tag == "Cubicle")
            {
                cube = go;
                break;
            }
        }

        if (cube == null)
        {
            return false;
        }

        agent.position3D = cube.transform.position;
        return true;
    }

    float timer = 0f;
    public override bool Pos_Perform()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 2f)
        {
            Debug.Log("Complete performing: " + actionName);
            this.isActive = false;
            ResourceManager.Instance.AddCube(cube);
            Nurse nurse = (Nurse)agent;
            nurse.inventory.Remove(cube);
            timer = 0;
        }
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

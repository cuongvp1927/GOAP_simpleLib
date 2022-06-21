using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StudentScript : CAgent
{
    [HideInInspector] NavMeshAgent navAgent;
    [HideInInspector] public List<GameObject> inventory;

    protected override void Awake()
    {
        navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        base.Awake();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using Unity.GOAP.World;
using UnityEngine;

public class Study : CActionBase
{
    public override bool HasCompleted(CAgent agent)
    {
        return CWorld.Instance.GetFacts().GetFact("relaxingTime").value == 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class Ate : CGoal
{
    public override void OnStart(CAgent agent)
    {
        base.OnStart(agent);
        Debug.Log(this.important);
    }
    public override void OnComplete(CAgent agent)
    {
        base.OnComplete(agent);
        try
        {
            LazyStudentSecVer student = (LazyStudentSecVer)agent;
            student.ResetHunger();
        }
        catch (System.MissingMethodException e)
        {
            Debug.LogError("No method ResetBoredom in object " + agent.agentName);
        }
    }
}

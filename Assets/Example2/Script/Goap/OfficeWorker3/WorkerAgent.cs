using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
public class WorkerAgent : CAgent
{
    Clock clock;
    protected override void Start()
    {
        base.Start();
        clock = FindObjectOfType<Clock>();
    }
    protected void Update()
    {
        int hour = clock.getHour();

        if (hour >= 24)
        {
            var i = Random.Range(0, 10);
            if (i < 4)
            {
                agentFact.ChangeFact("needToWorkNight", 1);
            }
            else
            {
                agentFact.ChangeFact("needToWorkNight", 0);
            }
            agentFact.ChangeFact("needMoney", 1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;
public class StudyingAfternoon : CGoal
{
    public StudyingAfternoon() : base()
    {
        this.goalName = "StudyingAfternoon";
    }

    public override void OnComplete(CAgent agent)
    {
        if (agent.goalList.FindIndex(f=>f.goalName == "SkipSchoolAndPlay") >= 0)
        {
            agent.UpdateGoalImportant("SkipSchoolAndPlay", 76);
            agent.UpdateGoalImportant(this.goalName, 50);
        }
        base.OnComplete(agent);
    }
}


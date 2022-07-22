using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.World;

public class LazyStudentSecVer : CAgent
{
    private int boredom = 0;
    private int skipCounter = 0;
    private int hunger = 0;


    [SerializeField] private int hungerIncPerSec = 20;
    [SerializeField] private int boredomIncPerSec = 20;
    
    [SerializeField] private int studyTimeMorning = 6;
    [SerializeField] private int studyTimeAfternoon = 13;

    Clock clock;

    public LazyStudentSecVer() : base()
    {
        this.agentName = "LazyStudentSecVer";
    }

    protected override void Start()
    {
        base.Start();
        
        clock = GameObject.FindObjectOfType<Clock>();
        UpdateFact(0);
    }

    float timer = 0f;
    int hour;
    protected void UpdateFact(float t)
    {
        float late = Random.Range(0f, 1f);
        if (((hour + t) >= (studyTimeMorning)) && hour < 11)
        {
            this.agentFact.ChangeFact("MorningStudyTime", 1);
        }
        else
        {
            this.agentFact.ChangeFact("MorningStudyTime", 0);

        }

        late = Random.Range(0f, 1f);
        if (((hour + t) >= (studyTimeAfternoon)) && hour < 17)
        {
            this.agentFact.ChangeFact("AfternoonStudyTime", 1);
        }
        else
        {
            this.agentFact.ChangeFact("AfternoonStudyTime", 0);
        }

        if ((hour>=22) ||(hour < studyTimeMorning))
        {
            this.agentFact.ChangeFact("SleepTime", 1);
        }
        else
        {
            this.agentFact.ChangeFact("SleepTime", 0);
        }

        if ((((hour >= 17) && (hour < 19))) || ((hour >= 21) && (hour < 22)))
        {
            this.agentFact.ChangeFact("FreeTime", 1);
        }
        else
        {
            this.agentFact.ChangeFact("FreeTime", 0);
        }

        if ((((hour>=11) && (hour< studyTimeAfternoon))) || ((hour >= 19) && (hour<21)))
        {
            this.agentFact.ChangeFact("EatTime", 1);
            this.agentFact.ChangeFact("FreeTime", 1);
        }
        else
        {
            this.agentFact.ChangeFact("EatTime", 0);
        }


    }

    public void ResetBoredom()
    {
        this.boredom = 0;
    }

    public void ResetHunger()
    {
        this.hunger = 0;
    }

    protected void Update()
    {
        hour = clock.getHour();
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;

            UpdateFact(timer);
            //Debug.Log("Fact:   ");

            //foreach (CFact f in agentFact.GetFactList())
            //{
            //    Debug.Log(f.name + ":  " + f.value);
            //}
            if (currentAction != null)
            {

                if (currentAction.name.Contains("Studying"))
                {
                    Debug.Log("Yes");
                    this.hunger += this.hungerIncPerSec;
                    this.boredom += this.boredomIncPerSec;
                }

                if (currentAction.name.Contains("Playing"))
                {
                    this.hunger += this.hungerIncPerSec;
                    this.boredom -= this.boredomIncPerSec;
                }

                int newImportant = this.hunger;
                this.UpdateGoalImportant("Ate", newImportant);

                newImportant = this.boredom;
                this.UpdateGoalImportant("Played", newImportant);

                if (hunger >= 110)
                {
                    this.InterruptCurrentAction();
                }
            }

        }



    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.GOAP.World;
using UnityEngine;

public enum DAY
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

public enum TimeFormat
{
    Hour_24,
    Hour_12
}
public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float secPerMin;
    [SerializeField] private TimeFormat _timeFormat = TimeFormat.Hour_24;
    [SerializeField] private int timeRatio;
    
    private int hour;
    private int minutes;

    private int maxHr = 24;
    private int maxMin = 60;

    private float timer = 0;
    private DAY day = DAY.Monday;

    private void Start()
    {
        AddFactToWorld();
    }

    private void Update()
    {
        if (timer >= secPerMin)
        {
            minutes += 1 * timeRatio;
            if (minutes >= maxMin)
            {
                minutes = 0;
                hour += 1;
                if (hour >= maxHr)
                {
                    hour = 0;
                    if ((int)(day + 1) == 7)
                    {
                        day = 0;
                    }
                    else
                    {
                        day += 1;
                    }
                }
                AddFactToWorld();
            }
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
        SetTimeDateSring();
    }

    void SetTimeDateSring()
    {
        switch (_timeFormat)
        {
            case TimeFormat.Hour_12:
                if (hour < 12)
                {
                    timeText.text = string.Format("{0}, {1}:{2} {3}", day.ToString(), hour, minutes, "AM");
                }
                else
                {
                    timeText.text = string.Format("{0}, {1}:{2} {3}", day.ToString(), hour - 12, minutes, "PM");
                }
                break;
            case TimeFormat.Hour_24:
                timeText.text = string.Format("{0}, {1}:{2}", day.ToString(), hour, minutes);
                break;
        }
    }

    private void AddFactToWorld()
    {
        if ((int)day < 4 && day >= 0)
        {
            CWorld.Instance.GetFacts().ChangeFact("workingDay", 1);
            CWorld.Instance.GetFacts().ChangeFact("offDay", 0);

            if (hour >= 7 && hour <= 17)
            {
                CWorld.Instance.GetFacts().ChangeFact("relaxingTime", 0);
            }
            else
            {
                CWorld.Instance.GetFacts().ChangeFact("relaxingTime", 1);
            }
        }
        else
        {
            CWorld.Instance.GetFacts().ChangeFact("offDay", 1);
            CWorld.Instance.GetFacts().ChangeFact("workingDay", 0);
            CWorld.Instance.GetFacts().ChangeFact("relaxingTime", 1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;


    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }


    void Update()
    {
        //text.text = string.Format("{0:00}:{1:00}", TimeManager.instance.hour, TimeManager.instance.minute);


        int day = TimeManager.instance.day;
        int hour = Mathf.RoundToInt(TimeManager.instance.hour);
        string time = "AM";
        if (hour >= 12) time = "PM";
        if (hour == 0) hour = 24;
        if (hour > 12) hour -= 12;
        if (hour < 10) time = " " + time;
        if (day == 0) day = day + 1;

        text.text = "Day " + day + "   " + hour + time;
    }
}

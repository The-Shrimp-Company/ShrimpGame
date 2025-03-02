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
        text.text = string.Format("{0:00}:{1:00}", TimeManager.instance.hour, TimeManager.instance.minute);
    }
}

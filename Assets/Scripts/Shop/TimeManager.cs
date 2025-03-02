using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float totalTime;
    [SerializeField] float secondsInADay;
    public int day = 1;
    public float hour;
    public int minute;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime / secondsInADay;

        day = 1 + Mathf.FloorToInt(totalTime);
        hour = Mathf.FloorToInt(totalTime * 24 % 24);
        minute = Mathf.FloorToInt(totalTime * 24 * 60 % 60);// 1440;
    }
}

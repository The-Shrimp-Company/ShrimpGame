using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [SerializeField] float secondsInADay;

    private float totalTime;
    public int day;
    public float hour;
    public int minute;


    public void Awake()
    {
        instance = this;
    }


    void Update()
    {
        totalTime += Time.deltaTime / secondsInADay;

        day = 1 + Mathf.FloorToInt(totalTime);
        hour = Mathf.FloorToInt(totalTime * 24 % 24);
        minute = Mathf.FloorToInt(totalTime * 1440 % 60);
    }
}

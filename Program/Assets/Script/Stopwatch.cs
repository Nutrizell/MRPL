using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    public float timeSurvived;

    bool stopwatchActive = false;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        stopwatchActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("TimerScript").GetComponent<Timer>().timeRemaining == 0)
        {
            stopwatchActive = false;
        }

        if (stopwatchActive)
        {
            timeSurvived += Time.deltaTime;
        }
    }

    
}
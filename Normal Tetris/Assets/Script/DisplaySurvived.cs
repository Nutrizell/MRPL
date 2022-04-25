using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySurvived : MonoBehaviour
{
    public Text stopwatchText;

    void Start()
    {
        
    }

    void Update()
    {
        displaySurvived(GameObject.Find("StopwatchScript").GetComponent<Stopwatch>().timeSurvived);
    }

    void displaySurvived(float TimetoDisplay)
    {
        TimetoDisplay += 1;

        float minutes = Mathf.FloorToInt(TimetoDisplay / 60);
        float seconds = Mathf.FloorToInt(TimetoDisplay % 60);

        stopwatchText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

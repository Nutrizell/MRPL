using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 30;
    public bool timerIsRunning = false;
    public Text timeText;
    public int timeOneLine = 10;
    public int timeTwoLine = 20;
    public int timeThreeLine = 40;
    public int timeFourLine = 80;
    public int ClearedRows = 0;

    private void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        UpdateTime();
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                SceneManager.LoadScene("GameOver");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    public void UpdateTime()
    {
        if (ClearedRows > 0)
        {
            if (ClearedRows == 1)
            {
                ClearedOneLine();
            }
            else if (ClearedRows == 2)
            {
                ClearedTwoLines();
            }
            else if (ClearedRows == 3)
            {
                ClearedThreeLines();
            }
            else if (ClearedRows == 4)
            {
                ClearedFourLines();
            }
            ClearedRows = 0;
        }
    }

    public void ClearedOneLine()
    {
        timeRemaining += timeOneLine;
    }

    public void ClearedTwoLines()
    {
        timeRemaining += timeTwoLine;
    }

    public void ClearedThreeLines()
    {
        timeRemaining += timeThreeLine;
    }

    public void ClearedFourLines()
    {
        timeRemaining += timeFourLine;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
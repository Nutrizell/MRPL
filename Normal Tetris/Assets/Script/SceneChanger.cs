using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public bool timeRunning = false;
    public float timeUntilChange = 3;

    // Start is called before the first frame update
    void Start()
    {
        timeRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunning)
        {
            if (timeUntilChange > 0)
            {
                timeUntilChange -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Survived");
                timeUntilChange = 0;
                timeRunning = false;
            }
        }
    }
}

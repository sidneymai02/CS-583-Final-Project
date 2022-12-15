using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText;
    private float startTime;
    private bool finished = false;
    private PlayerMovement pm;
    private float t;
    private string minutes;
    private string seconds;

    public ScoreTracker timeScore;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        pm = GetComponent<PlayerMovement>();
        timeScore = FindObjectOfType<ScoreTracker>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (finished) {
            return;
        }

        t = Time.time - startTime;
        minutes = ((int) t / 60).ToString();
        seconds = (t % 60).ToString("f2");

        timerText.text = "Timer: " + minutes + ":" + seconds;
    }
    public void Finish() {
        Debug.Log("Done in: " + minutes + " and " + seconds);
        pm.freeze = true;
        finished = true;
        timerText.color = Color.red;

        if (minutes.Equals("0"))
        {
            timeScore.score = timeScore.score + 3;
        }
        else
        {
            timeScore.score = timeScore.score + 1;
        }
    }
}

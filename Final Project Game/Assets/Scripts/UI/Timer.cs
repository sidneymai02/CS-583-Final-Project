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
    private PlayerGrapple pg;

    private float t;
    private string minutes;
    private string seconds;

    public ScoreTracker timeScore;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        pm = GetComponent<PlayerMovement>();
        pg = GetComponent<PlayerGrapple>();
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
        pm.freeze = true;
        pm.enabled = false;
        pg.enabled = false;
        finished = true;
        timerText.color = Color.red;

        //if (minutes.Equals("0"))
        //{
            //timeScore.score = timeScore.score + 10;
        //}
        //else if(minutes.Equals("1"))
        //{
            //timeScore.score = timeScore.score + 5;
        //}
       // else if(minutes.Equals("2")) {
            //timeScore.score = timeScore.score + 2;
        //}
    }
}

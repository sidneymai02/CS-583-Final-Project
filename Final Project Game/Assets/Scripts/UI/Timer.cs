using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText;
    private float startTime;
    private bool finished = false;
    private PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        pm = GetComponent<PlayerMovement>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (finished) {
            return;
        }

        float t = Time.time - startTime;
        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = "Timer: " + minutes + ":" + seconds;
    }
    public void Finish() {
        pm.freeze = true;
        finished = true;
        timerText.color = Color.red;
    }
}

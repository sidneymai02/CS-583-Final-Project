using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    public bool hasPowerup;
    public ScoreTracker s;
    // Start is called before the first frame update
    void Start()
    {
        s = FindObjectOfType<ScoreTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Powerup")) {
            hasPowerup = true;
            Destroy(other.gameObject);
            s.score = s.score + 1;
        }
    }
}

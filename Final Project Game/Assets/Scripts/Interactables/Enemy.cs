using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currentHealth = 3;
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

    public void Damage(int damageAmount) 
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) 
        {
            gameObject.SetActive(false);
            s.score = s.score + 1;
        }

    }
}

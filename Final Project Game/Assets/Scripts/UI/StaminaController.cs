using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    public float stamina;
    public float maxStamina;

    public Slider staminaBar;
    public float dValue;

    void Start()
    {
        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DecreaseEnergy();
        }
        else if (stamina != maxStamina)
        {
            IncreaseEnergy();
        }
        */
        staminaBar.value = stamina;
    }

    public void DecreaseEnergy()
    {
        if (stamina != 0 && stamina >= 0)
        {
            stamina -= dValue * Time.deltaTime;
        }
    }

    public void IncreaseEnergy()
    {
        if (stamina != maxStamina)
            stamina += dValue * Time.deltaTime;
    }
}

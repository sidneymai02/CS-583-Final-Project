using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu2 : MonoBehaviour
{
    public TextMeshProUGUI displayName;

    public void Awake()
    {
        displayName.text = Menu.menu1.nameUse;
    }

}

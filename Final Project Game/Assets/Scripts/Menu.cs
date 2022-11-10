using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public static Menu menu1;
    public TMP_InputField inputText;
    public string nameUse;

    private void Awake()
    {
        if (menu1 != null)
        {
            Destroy(gameObject);
            return;
        }

        menu1 = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveName()
    {
        nameUse = inputText.text;
        SceneManager.LoadSceneAsync("MainGame");

    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

}

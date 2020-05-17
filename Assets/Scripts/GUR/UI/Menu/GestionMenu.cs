﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GestionMenu : MonoBehaviour
{
    public Button[] mainButton;


    private bool isActivate;
    void Start()
    {
        isActivate = false;

        for (int i = 0; i < mainButton.Length; i++)
        {
            mainButton[i].gameObject.SetActive(false);
        }
    }

    
    void Update()
    {
        if (isActivate == false)
        {
            if (Input.GetButtonDown("Pause"))
            {
                isActivate = true;
                Time.timeScale = 0;

                for (int i = 0; i < mainButton.Length; i++)
                {
                    mainButton[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Pause"))
            {
                isActivate = false;
                Time.timeScale = 1;

                for (int i = 0; i < mainButton.Length; i++)
                {
                    mainButton[i].gameObject.SetActive(false);
                }
            }
        }
    }


    public void ResumeButton()
    {
        isActivate = false;
        Time.timeScale = 1;

        for (int i = 0; i < mainButton.Length; i++)
        {
            mainButton[i].gameObject.SetActive(false);
        }
    }


    public void QuitButton()
    {
        isActivate = false;
        Time.timeScale = 1;

        for (int i = 0; i < mainButton.Length; i++)
        {
            mainButton[i].gameObject.SetActive(false);
        }
        SceneManager.LoadScene(4);

    }
}
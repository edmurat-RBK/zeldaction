using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GestionMenu : MonoBehaviour
{
    public Button[] mainButton;

    [Header("Gestion Fade")]
    public float timeOfFade;
    public float timeBeforeSceneChange;

    private GameObject player;
    private GameObject lineRenderer;
    private GameObject fadeScreen;

    private bool isActivate;
    void Start()
    {
        fadeScreen = GameObject.FindWithTag("Fade");
        player = GameObject.FindWithTag("Player");
        lineRenderer = GameObject.FindWithTag("Line");

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
                PlayerManager.Instance.playerCanMove = false;
                PlayerManager.Instance.playerCanRotate = false;

                PlayerManager.Instance.lockcUseBucket = true;

                FindObjectOfType<AudioManager>().Stop("Fontaine");
                FindObjectOfType<AudioManager>().Stop("Arrosoire");
                FindObjectOfType<AudioManager>().Stop("Khameau");

                PlayerManager.Instance.playerRigidBody.velocity = Vector2.zero;
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

                //PlayerManager.Instance.lockcUseBucket = false;
                StartCoroutine(SmallcoolDown());

                PlayerManager.Instance.playerCanMove = true;
                PlayerManager.Instance.playerCanRotate = true;

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
        //PlayerManager.Instance.lockcUseBucket = false;
        StartCoroutine(SmallcoolDown());

        PlayerManager.Instance.playerCanMove = true;
        PlayerManager.Instance.playerCanRotate = true;
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

        FadeManager.Instance.FadeIn(fadeScreen, timeOfFade);
        StartCoroutine(CoolDOwnQuitButton());
    }

    IEnumerator SmallcoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerManager.Instance.lockcUseBucket = false;
    }

    IEnumerator CoolDOwnQuitButton()
    {
        yield return new WaitForSeconds(timeBeforeSceneChange);
        Destroy(player);
        SceneManager.LoadScene(0);
    }
}

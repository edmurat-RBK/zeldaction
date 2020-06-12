using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Manager;

public class DeathMenu : MonoBehaviour
{
    private GameObject player;
    private GameObject fadeScreen;

    private bool lockCoroutine;
    private void Start()
    {
        fadeScreen = GameObject.FindWithTag("Fade");
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetButtonDown("A"))
        {
            PlayerManager.Instance.GetComponent<PlayerHealth>().respawn();
        }

        if (Input.GetButtonDown("B"))
        {
            if (lockCoroutine == false)
            {
                lockCoroutine = true;
                StartCoroutine(Delay());
            }
            
        }
    }



    IEnumerator Delay()
    {
        Time.timeScale = 1;
        FadeManager.Instance.FadeIn(fadeScreen, 1f);
        yield return new WaitForSecondsRealtime(1.1f);
        Destroy(player);
        SceneManager.LoadScene(0);
    }
}

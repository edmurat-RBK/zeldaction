using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Manager;

public class DeathMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("X"))
        {
            PlayerManager.Instance.GetComponent<PlayerHealth>().respawn();
        }

        if (Input.GetButtonDown("B"))
        {
            SceneManager.LoadScene(4);
        }
    }
}

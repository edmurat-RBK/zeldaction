using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Manager;

public class DeathMenu : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
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
            Destroy(player);
            SceneManager.LoadScene(0);
        }
    }
}

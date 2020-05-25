using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Manager;

public class DeathMenu : MonoBehaviour
{
    public void RestartGame()
    {
        PlayerManager.Instance.GetComponent<PlayerHealth>().respawn();
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(4);
    }
}

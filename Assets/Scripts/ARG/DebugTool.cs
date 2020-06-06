using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.SceneManagement;

public class DebugTool : MonoBehaviour
{
    private bool isInvinsible = false;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isInvinsible == false)
            {

                isInvinsible = true;
                PlayerManager.Instance.playerInvulnerable = true;
            }
            else
            {
                isInvinsible = false;
                PlayerManager.Instance.playerInvulnerable = false;
            }
        }

        //Scène extérieur village
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PlayerManager.Instance.playerTransform.position = new Vector2(44.67f, 40.62f);
            SceneManager.LoadScene(2);
        }

        //Scène extérieur première zone
        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerManager.Instance.playerTransform.position = new Vector2(0.02f, 75.89f);
            SceneManager.LoadScene(2);
        }

        //Scène extérieur deuxième zone
        if (Input.GetKeyDown(KeyCode.F3))
        {
            PlayerManager.Instance.playerTransform.position = new Vector2(-37.16f, 109.84f);
            SceneManager.LoadScene(2);
        }

        //Scène extérieur volcan
        if (Input.GetKeyDown(KeyCode.F4))
        {
            PlayerManager.Instance.playerTransform.position = new Vector2(-35.55f, 148.85f);
            SceneManager.LoadScene(2);
        }

        //Scène donjon chaman
        if (Input.GetKeyDown(KeyCode.F5))
        {
            PlayerManager.Instance.playerTransform.position = new Vector2(72.63f, 72.73f);
            SceneManager.LoadScene(3);
        }

        //Scène donjon volcan
        if (Input.GetKeyDown(KeyCode.F6))
        {
            PlayerManager.Instance.playerTransform.position = new Vector2(-9.5f, 154.6f);
            SceneManager.LoadScene(4);
        }

        //Scène boss
        if (Input.GetKeyDown(KeyCode.F7))
        {
            PlayerManager.Instance.playerTransform.position = new Vector2(0.28f, -14.42f);
            SceneManager.LoadScene(5);
        }
    }
}

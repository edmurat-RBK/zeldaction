using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Manager;

public class SceneLoader : MonoBehaviour
{
    private bool canSwitch;
    public int numberOfScene;

    public float axeX;
    public float axeY;

    
    void Start()
    {
        canSwitch = false;
    }

   
    void Update()
    {
        if (canSwitch == true)
        {
            if (Input.GetButtonDown("X"))
            {
                canSwitch = false;
                SceneManager.LoadScene(numberOfScene);
                PlayerManager.Instance.playerTransform.position = new Vector2(axeX, axeY);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canSwitch = false;
        }
    }

}

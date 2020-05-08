using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Manager;

public class SceneLoader : MonoBehaviour
{
    public bool canSwitch;
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
                PlayerManager.Instance.playerTransform.position = new Vector2(axeX, axeY);
                canSwitch = false;
                SceneManager.LoadScene(numberOfScene);
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

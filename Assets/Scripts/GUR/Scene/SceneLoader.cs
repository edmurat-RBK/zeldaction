using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Manager;

public class SceneLoader : MonoBehaviour
{
    public GameObject sprite;

    private bool canSwitch;
    public int numberOfScene;

    public float axeX;
    public float axeY;

    public GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sprite.SetActive(false);
        canSwitch = false;
    }

   
    void Update()
    {
        if (canSwitch == true)
        {
            if (Input.GetButtonDown("X"))
            {
                canSwitch = false;

                player.GetComponent<HealthBar>().lockCanTake = false;
                PlayerManager.Instance.playerTransform.position = new Vector2(axeX, axeY);
                SceneManager.LoadScene(numberOfScene);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canSwitch = true;
            sprite.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canSwitch = false;
            sprite.SetActive(false);
        }
    }

}

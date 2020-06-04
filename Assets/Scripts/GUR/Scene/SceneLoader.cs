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
        PlayerManager.Instance.isArroisoir = false;
        PlayerManager.Instance.isAttacking = false;
        PlayerManager.Instance.isFontaine = false;
        PlayerManager.Instance.isKhameau = false;

        PlayerManager.Instance.onDirt = false;
        PlayerManager.Instance.onSand = false;
        PlayerManager.Instance.onConcrete = false;


        player = GameObject.FindWithTag("Player");
        sprite.SetActive(false);
        canSwitch = false;
    }

   
    void Update()
    {
        if (canSwitch == true)
        {
            if (PlayerManager.Instance.lockcUseBucket == false)
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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
    public bool savePoint;

    public float speed;

    public GameObject hitBox;
    public float timeForOpen;

    private Vector2 direction;
    private Rigidbody2D rbDoor;
    public bool canOpen;
    void Start()
    {
        rbDoor = gameObject.GetComponent<Rigidbody2D>();
        canOpen = true;
        direction = Vector2.up;   
    }

    void Update()
    {
        if (gameObject.GetComponent<GestionActivateur>().canActive == true && canOpen == true)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        canOpen = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder += 1;
        rbDoor.velocity = direction * speed * Time.fixedDeltaTime;
        yield return new WaitForSeconds(timeForOpen);
        rbDoor.velocity = Vector2.zero;
        hitBox.SetActive(false);

        
        if (savePoint == true)
        {
            
            gameObject.GetComponent<UpgradeObject>().Gotcha();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
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
        rbDoor.velocity = direction * speed * Time.fixedDeltaTime;
        yield return new WaitForSeconds(timeForOpen);
        rbDoor.velocity = Vector2.zero;
        hitBox.SetActive(false);
        
    }
}

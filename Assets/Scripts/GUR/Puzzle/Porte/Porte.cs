using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
    public float speed;
    public GameObject[] wichPlaque;
    public float timeForOpen;

    private Vector2 direction;
    private Rigidbody2D rbDoor;
    private int numberOfActive;
    private bool canOpen;
    void Start()
    {
        rbDoor = gameObject.GetComponent<Rigidbody2D>();
        canOpen = true;
        numberOfActive = 0;
        direction = Vector2.up;   
    }

    void Update()
    {
        for (int i = 0; i < wichPlaque.Length; i++)
        {
            if (wichPlaque[i].GetComponent<PlaqueDePression>().activeTrap == true)
            {
                numberOfActive += 1;
            }

            if (numberOfActive == wichPlaque.Length && canOpen == true)
            {
                StartCoroutine(OpenDoor());
            }
        }
        numberOfActive = 0;
    }

    IEnumerator OpenDoor()
    {
        canOpen = false;
        rbDoor.velocity = direction * speed * Time.fixedDeltaTime;
        yield return new WaitForSeconds(timeForOpen);
        rbDoor.velocity = Vector2.zero;
    }
}

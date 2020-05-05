using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvDetection : MonoBehaviour
{
    public bool canMakeMove;

    private void Start()
    {
        canMakeMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 16)
        {
            canMakeMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 16)
        {
            canMakeMove = false;
        }
    }
}

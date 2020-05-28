using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionObsi : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 21)
        {
            GetComponentInParent<Obsidian>().playerOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 21)
        {
            GetComponentInParent<Obsidian>().playerOn = false;
        }
    }
}

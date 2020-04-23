using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAggro : MonoBehaviour
{
    public bool canAggro = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 31)
        {
            canAggro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 31)
        {
            canAggro = false;
        }
    }
}

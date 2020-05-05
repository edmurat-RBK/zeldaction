using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCaisse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cube de bois")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<CubeBois>().notStop = false;
        }

        if (collision.gameObject.tag == "CaissePierre")
        {

        }
    }
}

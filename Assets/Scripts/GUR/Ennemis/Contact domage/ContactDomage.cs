using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDomage : MonoBehaviour
{
    public int damage;
    public bool destroyOnCollision = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Je rentre");
            collision.gameObject.GetComponent<PlayerHealth>().TakeHit(damage);

            if (destroyOnCollision == true)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().health += 1;
            collision.gameObject.GetComponent<HealthBar>().HealthSysteme();
            Destroy(gameObject);
        }
    }
}

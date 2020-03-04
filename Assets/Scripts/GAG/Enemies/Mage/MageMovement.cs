using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMovement : MonoBehaviour
{
    [Header ("Stats de base")]
    public float pv;
    public float speed;

    [Header ("Valeur de déplacement")]
    public float stoppingDistance;
    public float retreatDistance;

    [Space]
    public Transform player;

    Vector2 movement;
    Vector2 retreat;

    void Start()
    {
   
    }

    void Update()
    {
        MageDisplacement();
    }

    private void MageDisplacement()
    {
        movement = (player.transform.position - transform.position).normalized;
        retreat = (transform.position - player.transform.position).normalized;

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            GetComponent<Rigidbody2D>().velocity = (movement.normalized * speed * Time.fixedDeltaTime);
        }

        else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.transform.position) > retreatDistance)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        else if (Vector2.Distance(transform.position, player.transform.position) < retreatDistance)
        {
            GetComponent<Rigidbody2D>().velocity = (retreat.normalized * speed * Time.fixedDeltaTime);
        }

    }

}

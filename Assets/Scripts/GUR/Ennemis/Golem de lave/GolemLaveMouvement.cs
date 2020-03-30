using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class GolemLaveMouvement : MonoBehaviour
{
    [Header("Stats de base")]
    public float pv;
    public float speed;
    public float timeBeforeAttack;
    public float attackCooldown;

    [Header("Valeur de déplacement")]
    public float stoppingDistance;
    public float retreatDistance;

    Vector2 movement;

    private bool lockMouvement;
    private bool lockAttack ;

    private Transform player;
    private Rigidbody2D rbGolem;

    void Start()
    {
        lockMouvement = true;
        lockAttack = true;
        rbGolem = GetComponent<Rigidbody2D>();
        player = PlayerManager.Instance.transform;
    }

    void Update()
    {
        if (GetComponentInChildren<ZoneAggro>().canAggro == true)
        {
            GolemLaveDisplacement();
        }

        if (GetComponentInChildren<ZoneAggro>().canAggro == false)
        {
            rbGolem.velocity = Vector2.zero;
        }
    }

    void GolemLaveDisplacement()
    {
        movement = (player.transform.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            rbGolem.velocity = (movement.normalized * speed * Time.fixedDeltaTime);
        }
        
        else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.transform.position) > retreatDistance)
        {
            rbGolem.velocity = Vector2.zero;
        }
    }


}

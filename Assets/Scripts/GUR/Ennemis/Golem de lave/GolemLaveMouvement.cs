using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class GolemLaveMouvement : MonoBehaviour
{
    #region Variable
    [Header("Stats de base")]
    public float pv;
    public float speed;
    public float timeBeforeAttack;
    public float attackCooldown;

    [Header("Valeur de déplacement")]
    public float stoppingDistance;
    public float retreatDistance;

    [Header ("Onnde de choc")]
    public GameObject chocWave;

    public GameObject shotPoint;

    Vector2 movement;

    private bool lockMouvement;
    private bool lockAttack ;

    private Transform player;
    private Rigidbody2D rbGolem;
    #endregion
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

        if (lockMouvement == true)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                rbGolem.velocity = (movement.normalized * speed * Time.fixedDeltaTime);
            }
        
            else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.transform.position) > retreatDistance)
            {
                rbGolem.velocity = Vector2.zero;

                if (lockAttack == true)
                {
                    StartCoroutine(GolemLaveAtack());
                }
            }

        }
    }

    IEnumerator GolemLaveAtack()
    {
        lockMouvement = false;
        yield return new WaitForSeconds(timeBeforeAttack);

        var dir = new Vector2 (player.transform.position.x - shotPoint.transform.position.x, player.transform.position.y - shotPoint.transform.position.y);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject wave = Instantiate(chocWave, shotPoint.transform.position, shotPoint.transform.rotation);
        Destroy(wave, 0.5f);
        StartCoroutine(AttackCoolDown());
        lockMouvement = true;
    }

    IEnumerator AttackCoolDown()
    {
        lockAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        lockAttack = true;
    }
   
}

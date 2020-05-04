using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

/// <summary>
/// Créateur : Gauthier Gobert 
/// Le script permet de : 
/// - Déplacer le mage en fonction de la position du joueur
/// - Faire spawn un projectile sur la position du joueur 
/// - D'activer le mage quand le joueur rentre dans sa zone d'aggro
/// </summary>

public class MageMovement : MonoBehaviour
{
    #region Variable
    [Header ("Stats de base")]
    public float pv;
    public float speed;
    public float timeBeforeAttack;
    public float attackCooldown;

    [Header ("Valeur de déplacement")]
    public float stoppingDistance;
    public float retreatDistance;

    [Space]
    private Transform player;
    public GameObject projectile;

    Vector2 movement;
    Vector2 retreat;

    private Animator anim;
    private Rigidbody2D rbMage;

    public bool vunerableMage;

    [HideInInspector]
    public bool deathLockMage;

    private bool lockMovement = true;
    public bool lockAttack = true;
    #endregion
    
    private void Start()
    {
        rbMage = GetComponent<Rigidbody2D>();
        deathLockMage = false;
        player = PlayerManager.Instance.transform;
        anim = GetComponent<Animator>();
        vunerableMage = false;
        StartCoroutine(Cooldown());
    }


    void Update()
    {
        
        if (GetComponentInChildren<ZoneAggro>().canAggro == true)
        {
            MageDisplacement();
        }

        if (GetComponentInChildren<ZoneAggro>().canAggro == false)
        {
            rbMage.velocity = Vector2.zero;
        }

        if (vunerableMage == true)
        {
            rbMage.velocity = Vector2.zero;
            StopAllCoroutines();
            lockMovement = true;
            lockAttack = true;
        }
    }

    private void MageDisplacement()
    {
        movement = (player.transform.position - transform.position).normalized;
        retreat = (transform.position - player.transform.position).normalized;

       
        if (lockMovement == true && deathLockMage == false)
        {
            if (vunerableMage == false)
            {
                //youmna a ecrit ca
                anim.SetFloat("Horizontal", rbMage.velocity.x);
                anim.SetFloat("Vertical", rbMage.velocity.y);


                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    
                    anim.SetBool("IsRecule", false);

                    rbMage.velocity = (movement.normalized * speed * Time.fixedDeltaTime);

                }

                else if (Vector2.Distance(transform.position, player.transform.position) < retreatDistance)
                {

                    rbMage.velocity = (retreat.normalized * speed * Time.fixedDeltaTime);

                    anim.SetBool("IsRecule", true);

                }

                else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.transform.position) > retreatDistance)
                {
                    rbMage.velocity = Vector2.zero;

                    if (lockAttack == true)
                    {
                        StartCoroutine(Attack());
                    }
                }

            }

        }
    }  // Fonction qui permet le déplacement du mage

    IEnumerator Attack()
    {
        anim.SetBool("IsAttacking", true);

        lockMovement = false;
        yield return new WaitForSeconds(timeBeforeAttack);
        anim.SetBool("IsAttacking", false);
        GameObject meteor = Instantiate(projectile, player.transform.position, transform.rotation);
        StartCoroutine(Cooldown());
        yield return new WaitForSeconds(0.3f);

        lockMovement = true;


    } // Coroutine qui permet de faire spawn un projectile sur la position du joueur après une incantation

    IEnumerator Cooldown()
    {
        lockAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        lockAttack = true;
    } // Coroutine qui gère le cooldown après chaque attaque
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Transform player;
    public GameObject projectile;

    Vector2 movement;
    Vector2 retreat;

    private bool lockMovement = true;
    private bool lockAttack = true;
    #endregion

    void Update()
    {
        if (GetComponentInChildren<ZoneAggro>().canAggro == true)
        {
            MageDisplacement();
        }

        if (GetComponentInChildren<ZoneAggro>().canAggro == false)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void MageDisplacement()
    {
        movement = (player.transform.position - transform.position).normalized;
        retreat = (transform.position - player.transform.position).normalized;

        if (lockMovement == true)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                GetComponent<Rigidbody2D>().velocity = (movement.normalized * speed * Time.fixedDeltaTime);
            }

            else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.transform.position) > retreatDistance)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                if (lockAttack == true)
                {
                    StartCoroutine(Attack());
                }
            }

            else if (Vector2.Distance(transform.position, player.transform.position) < retreatDistance)
            {
                GetComponent<Rigidbody2D>().velocity = (retreat.normalized * speed * Time.fixedDeltaTime);
            }
        }
    }  // Fonction qui permet le déplacement du mage

    IEnumerator Attack()
    {
        lockMovement = false;
        yield return new WaitForSeconds(timeBeforeAttack);
        GameObject meteor = Instantiate(projectile, (player.transform.position + new Vector3(0,2.5f)), transform.rotation);
        StartCoroutine(Cooldown());
        lockMovement = true;

    } // Coroutine qui permet de faire spawn un projectile sur la position du joueur après une incantation

    IEnumerator Cooldown()
    {
        lockAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        lockAttack = true;
    } // Coroutine qui gère le cooldown après chaque attaque
}

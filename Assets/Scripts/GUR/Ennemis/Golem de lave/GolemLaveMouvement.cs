using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

/// <summary>
/// Créateur Guillaume Rogé 
/// Ce script permet de : 
/// - Une fois le joueur dans la zone d'aggro d'activer le golem
/// - Déplacer le golem vers le joueur selon ne vitesse donné
/// - Une fois à une certaine distance arrete le golem pour le faire attaquer
/// - Gérer un systéme de cooldown d'attaque
/// </summary>
public class GolemLaveMouvement : MonoBehaviour
{
    #region Variable
    [Header("Stats de base")]
    public float timeBeforeAttack;
    public float attackCooldown;

    [Header("Valeur de déplacement")]
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    [Header ("Onnde de choc")]
    public GameObject chocWave;

    public GameObject shotPoint;

    public bool vunerableGolem;

    Vector2 movement;

    [HideInInspector]
    public bool deathLockGolem;

    private bool lockMouvement;
    private bool lockAttack ;

    private Transform player;
    private Rigidbody2D rbGolem;

    private Animator anim;

    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();

        deathLockGolem = false;
        lockMouvement = true;
        lockAttack = true;
        vunerableGolem = false;
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
            anim.SetBool("IsWalking", false);
        }

        if (vunerableGolem == true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StopAllCoroutines();
            lockMouvement = true;
            lockAttack = true;
            rbGolem.isKinematic = false;
        }
    }

    void GolemLaveDisplacement()
    {
        movement = (player.transform.position - transform.position).normalized;

        if (vunerableGolem == false)
        {
            if (lockMouvement == true)
            {
                
                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    rbGolem.velocity = (movement.normalized * speed * Time.fixedDeltaTime);

                    anim.SetBool("IsWalking", true);

                    anim.SetFloat("Horizontal", GetComponent<Rigidbody2D>().velocity.x);
                    anim.SetFloat("Vertical", GetComponent<Rigidbody2D>().velocity.y);

                }
        
                else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.transform.position) > retreatDistance)
                {
                    rbGolem.velocity = Vector2.zero;

                    anim.SetBool("IsWalking", false);

                    if (lockAttack == true)
                    {
                        StartCoroutine(GolemLaveAtack());
                    }
                }

            }

        }
    } // Fonction qui déplace le golem vers le joueur selon une vitesse donné et l'arrête à une distance donné

    IEnumerator GolemLaveAtack()
    {
        anim.SetBool("IsAttacking", true);

        lockMouvement = false;
        yield return new WaitForSeconds(timeBeforeAttack);

        var dir = new Vector2 (player.transform.position.x - shotPoint.transform.position.x, player.transform.position.y - shotPoint.transform.position.y); // Permet d'orienter le shotPoint vers le joueur
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                          
        shotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rbGolem.isKinematic = true;

        FindObjectOfType<AudioManager>().Play("AttackGolem");

        GameObject wave = Instantiate(chocWave, shotPoint.transform.position, shotPoint.transform.rotation); // spawn l'onde de choc
        Destroy(wave, 0.5f);
        StartCoroutine(AttackCoolDown());
        yield return new WaitForSeconds(1);
        rbGolem.isKinematic = false;
        lockMouvement = true;
    } // Coroutine qui fais attaquer le golem en faisant spawn l'onde de choc sur un spawn point

    IEnumerator AttackCoolDown()
    {
        lockAttack = false;

        anim.SetBool("IsAttacking", false);

        yield return new WaitForSeconds(attackCooldown);
        
        lockAttack = true;
    } // Coroutine qui empéche le golem d'attaquer juste après une attaque
   
}

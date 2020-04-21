using System.Collections; using System.Collections.Generic; using UnityEngine; using Manager;  /// <summary> /// Créateur : Gauthier Gobert  /// Le script permet de :  /// - Déplacer le mage en fonction de la position du joueur /// - Faire spawn un projectile sur la position du joueur  /// - D'activer le mage quand le joueur rentre dans sa zone d'aggro /// </summary>  public class MageMovement : MonoBehaviour {     #region Variable     [Header ("Stats de base")]     public float pv;     public float speed;     public float timeBeforeAttack;     public float attackCooldown;      [Header ("Valeur de déplacement")]     public float stoppingDistance;     public float retreatDistance;      [Space]     private Transform player;     public GameObject projectile;      Vector2 movement;     Vector2 retreat;

    private Animator anim;      private bool lockMovement = true;     private bool lockAttack = true;     #endregion          private void Start()     {         player = PlayerManager.Instance.transform;         anim = GetComponent<Animator>();              }       void Update()     {         if (GetComponentInChildren<ZoneAggro>().canAggro == true)         {             MageDisplacement();         }          if (GetComponentInChildren<ZoneAggro>().canAggro == false)         {             GetComponent<Rigidbody2D>().velocity = Vector2.zero;         }     }      private void MageDisplacement()     {         movement = (player.transform.position - transform.position).normalized;         retreat = (transform.position - player.transform.position).normalized;          //youmna a ecrit ca          anim.SetFloat("Horizontal",GetComponent<Rigidbody2D>().velocity.x);         anim.SetFloat("Vertical", GetComponent<Rigidbody2D>().velocity.y);          if (lockMovement == true)         {             if (Vector2.Distance(transform.position, player.position) > stoppingDistance)             {                 anim.SetBool("IsRecule", false);                  GetComponent<Rigidbody2D>().velocity = (movement.normalized * speed * Time.fixedDeltaTime);

public class MageMovement : MonoBehaviour
{
    #region Variable
    [Header ("Stats de base")]
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

    public bool vunerableMage;

    private bool lockMovement = true;
    private bool lockAttack = true;
    #endregion
    
    private void Start()
    {
        player = PlayerManager.Instance.transform;
        anim = GetComponent<Animator>();
        vunerableMage = false;
    }


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

        if (vunerableMage == true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StopAllCoroutines();
            lockMovement = true;
            lockAttack = true;
        }

    }

    private void MageDisplacement()
    {
        if (vunerableMage == false)
        {
            movement = (player.transform.position - transform.position).normalized;
            retreat = (transform.position - player.transform.position).normalized;

       
            if (lockMovement == true)
            {
                //youmna a ecrit ca

                anim.SetFloat("Horizontal", GetComponent<Rigidbody2D>().velocity.x);
                anim.SetFloat("Vertical", GetComponent<Rigidbody2D>().velocity.y);


                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    anim.SetBool("IsRecule", false);

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

                    anim.SetBool("IsRecule",true);

                }
            }
        }
    }  // Fonction qui permet le déplacement du mage

    IEnumerator Attack()
    {
        anim.SetBool("IsAttacking", true);

        lockMovement = false;
        yield return new WaitForSeconds(timeBeforeAttack);
        GameObject meteor = Instantiate(projectile, (player.transform.position + new Vector3(0,2.5f)), transform.rotation);
        StartCoroutine(Cooldown());
        lockMovement = true;

        anim.SetBool("IsAttacking", false);

    } // Coroutine qui permet de faire spawn un projectile sur la position du joueur après une incantation

    IEnumerator Cooldown()
    {
        lockAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        lockAttack = true;
    } // Coroutine qui gère le cooldown après chaque attaque
}

using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manager health of the player
/// by Edouard Murat and Arthur Galland
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    #region Variables
    public int maximumHealth;
    public int health;
    private bool isDead;

    public List<GameObject> respawnPoints = new List<GameObject>();
    public GameObject actualRespawnPoint; //give him one in the inspector
    private bool alreadyInList;
    private PlayerManager playerManager;

    public Sprite checkpointBaseSprite;
    public Sprite checkpointActiveSprite;
    private Animator anim;
    
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        playerManager.healthPlayer = health;
        playerManager.healthMax = maximumHealth;

        //health = playerManager.healthPlayer;
        //maximumHealth = playerManager.healthMax;

        if (Input.GetKeyDown(KeyCode.U))
        {
            health += 1;
            gameObject.GetComponent<HealthBar>().HealthSysteme();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
           TakeDamage(1);
        }
    }

    // Function that check player vulnerability before taking damage
    // Called when the avatar is hit by an enemy
    public void TakeHit(int damage)
    {

        if(!playerManager.playerInvulnerable)
        {
            TakeDamage(damage);
            
        }
    }

    // Function that give damage to player health
    // Called in TakeHit()
    public void TakeDamage(int damage)
    {
        if (playerManager.canTakeDammage == true)
        {
            //animation stagger
            anim.SetTrigger("IsDamaged");
            health -= damage;
            gameObject.GetComponent<HealthBar>().HealthSysteme();
            StartCoroutine("Invulnerability");
            if (health <= 0)
            {
                health = 0;
                StartCoroutine("Death");
            }
        }

    }

    // Function called at player death
    // Called in TakeDamage()
    private IEnumerator Death()
    {
        isDead = true;
        anim.SetBool("IsDead",true);
        playerManager.playerCanMove = false;
        playerManager.playerRigidBody.velocity = Vector2.zero;
        playerManager.deathParalise = true;
        playerManager.deathParalisy(); //player can't use his action but sprite without bucket
        //animation death
        playerManager.getBucket = false;
        playerManager.obtainBucket(); //player can't use his action but sprite without bucket
        yield return new WaitForSeconds(2f);//animation time
        playerManager.playerCanMove = true;
        playerManager.deathParalise = false;
        playerManager.deathParalisy();
        respawn();
    }

    // Function that restore health
    // Called when the player use an item that restore health
    public void Heal(int amount)
    {
        if(!isDead)
        {
            health += amount;
            if(health >= maximumHealth)
            {
                health = maximumHealth;
            }
        }
    }

    //Function for respawn at the actual checkpoint with full health
    public void respawn()
    {
        anim.SetBool("IsDead", false);
        transform.position = actualRespawnPoint.transform.position;
        health = maximumHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10) //add only one copy of each checkpoint in the list
        {
            foreach (GameObject respawnPoint in respawnPoints)
            {
                if (collision.gameObject == respawnPoint)
                {
                    alreadyInList = true;
                }
            }

            if (!alreadyInList)
            {
                respawnPoints.Add(collision.gameObject);
            }
            alreadyInList = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10) //fonction for saving a checkpoint
        {
            if (Input.GetButtonDown("X"))
            {
                actualRespawnPoint = collision.gameObject;
                foreach(GameObject respawnPoint in respawnPoints) //change all the other sprite of checkpoint not used by the player
                {
                    respawnPoint.GetComponent<SpriteRenderer>().sprite = checkpointBaseSprite;
                }
                actualRespawnPoint.GetComponent<SpriteRenderer>().sprite = checkpointActiveSprite;
                //animation checkpoint + message
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        playerManager.playerInvulnerable = true;
        playerManager.canTakeDammage = false;
        yield return new WaitForSeconds(2);
        playerManager.playerInvulnerable = false;
        playerManager.canTakeDammage = true;
    }

}

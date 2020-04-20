using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manager health of the player
/// by Edouard Murat
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public float maximumHealth = 3f;
    public float health = 3f;
    private bool isDead;

    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        health = maximumHealth;
        playerManager = GetComponent<PlayerManager>();
    }

    // Function that check player vulnerability before taking damage
    // Called when the avatar is hit by an enemy
    public void TakeHit(float damage)
    {
        if(!playerManager.playerInvulnerable)
        {
            TakeDamage(damage);
        }
    }

    // Function that give damage to player health
    // Called in TakeHit()
    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    // Function called at player death
    // Called in TakeDamage()
    private void Die()
    {
        isDead = true;
    }

    // Function that restore health
    // Called when the player use an item that restore health
    public void Heal(float amount)
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
}

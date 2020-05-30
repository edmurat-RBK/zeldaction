using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de : 
///  - Détruire les caisse qui touche cet objet
/// </summary>
public class CaisseDestructor : MonoBehaviour
{
    public GameObject respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cube de bois")
        {
            FindObjectOfType<AudioManager>().Play("CaisseB death");

            if (collision.gameObject.GetComponent<CubeBois>().playerOnIt == true)
            {
                PlayerManager.Instance.playerTransform.position = respawnPoint.transform.position;
                PlayerManager.Instance.playerRigidBody.velocity = Vector2.zero;
            }

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "CaissePierre")
        {
            Destroy(collision.gameObject);
        }
    }
}

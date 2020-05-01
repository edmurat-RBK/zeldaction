using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

/// <summary>
///Créateur : Guillaume Rogé
/// - Ce script permet de : 
/// - Déplacer l'axe z rotation petit à petit 
/// - Faire spawn un projectile dans la direction de l'axe z
/// - Tout cela répéter un nombre de fois par le nombre de projectiles voulu
/// </summary>

public class OndeChoc : MonoBehaviour
{
    #region Variable
    [Header("Gestion du cône")]
    public float step;
    public float start;
    public int bulNumber;
   
    [Header ("Projectile tiré")]
    public GameObject bulletPrefab;

    [Header ("Vitesse des projectiles")]
    public float speed;

    Vector2 direction;
    public GameObject bulletParent;
    #endregion

    private void Start()
    {
        Fire(); // Lance la fonction de tire
    }

    void Fire()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - start);
        
        for (int i = 0; i < bulNumber; i++)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + step);
            direction = transform.right;

            GameObject bul = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //bul.transform.SetParent(bulletParent.transform);
            bul.GetComponent<Rigidbody2D>().velocity = direction * speed * Time.fixedDeltaTime;

            Destroy(bul, 3f);
        }

    } // Fonction qui permet de faire spawn des projectiles selon l'axe z
}
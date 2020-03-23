using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé 
/// Ce script permet de : 
/// - Donné une force à la caisse de bois dans une direction choisi
/// </summary>
public class CourantEau : MonoBehaviour
{
    #region Variable
    [Header ("Mettre la direction du courant d'eau")]
    public string direction;
    [Header ("Vitesse de déplacement des caisses sur le courant")]
    public float waterSpeed;

    [HideInInspector]
    public Vector2 movement;
    #endregion

    void Start()
    {
        switch (direction) 
        {
            case("Bas"):
                movement = Vector2.down;
                break;

            case ("Haut"):
                movement = Vector2.up;
                break;

            case ("Droite"):
                movement = Vector2.right;
                break;

            case ("Gauche"):
                movement = Vector2.left;
                break;
        } // Permet de donné la direction choisi dans l'inspecteur au vecteur
    }

    private void OnTriggerStay2D(Collider2D other) // Permet de donné une vélocité à un cube de bois quand il est dans le courant
    {
        if (other.gameObject.layer == 28) // 28 = layer Cube de bois
        {
            if(other.GetComponent<CubeBois>().canRedirect == true)
            {
                other.GetComponent<CubeBois>().canRedirect = false;
                other.GetComponent<Rigidbody2D>().velocity = (movement.normalized * waterSpeed * Time.fixedDeltaTime);
                other.GetComponent<CubeBois>().wichDirection = direction;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Permet d'être sur qu'un cube soit bien sortie d'un courant avant d'en prendre un autres
    {

        other.GetComponent<CubeBois>().canRedirect = true;
    }
}

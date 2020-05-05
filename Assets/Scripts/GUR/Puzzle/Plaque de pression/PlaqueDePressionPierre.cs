 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé 
/// Ce Script permet de : 
/// - De detecter la hitbox du joueur quand il est sur la plaque et quand il part
/// - Changer graphiquement la plaque quand elle est activé ou non
/// - Changer une bool qui permet d'activer les piège relier
/// </summary>

public class PlaqueDePressionPierre : MonoBehaviour
{
    #region Variable
    [Header ("Glisser les gameobject qui contienne les grph")]
    public GameObject plaqueActive;
    public GameObject plaqueDesactive;

    public bool stayActivate;

    [HideInInspector]
    public bool activePlaquePierre;
    #endregion

    void Start()
    {
        activePlaquePierre = false;
        plaqueActive.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "CaissePierre")
        {
            plaqueActive.SetActive(true);
            plaqueDesactive.SetActive(false);
            activePlaquePierre = true;
        }
    } // Permet de changé graphiquement la plaque (activé) et de changé l'etat de la bool en true

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (stayActivate == false)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "CaissePierre")
            {
                plaqueActive.SetActive(false);
                plaqueDesactive.SetActive(true);
                activePlaquePierre = false;
            }
        }
    } // Permet de changé graphiquement la plaque (désactivé) et de changé l'etat de la bool en false
}

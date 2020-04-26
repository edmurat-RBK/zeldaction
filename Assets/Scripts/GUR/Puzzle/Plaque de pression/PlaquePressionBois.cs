using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaquePressionBois : MonoBehaviour
{
    [Header("Glisser les gameobject qui contienne les grph")]
    public GameObject plaqueActive;
    public GameObject plaqueDesactive;

    public bool stayActivate;

    [HideInInspector]
    public bool activePlaqueBois;

    void Start()
    {
        activePlaqueBois = false;
        plaqueActive.SetActive(false);
    }

    void Update()
    {


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        plaqueActive.SetActive(true);
        plaqueDesactive.SetActive(false);
        activePlaqueBois = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (stayActivate == false)
        {
            plaqueActive.SetActive(false);
            plaqueDesactive.SetActive(true);
            activePlaqueBois = false;
        }
    }
}

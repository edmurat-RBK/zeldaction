using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaquePressionBois : MonoBehaviour
{
    [Header("Glisser les gameobject qui contienne les grph")]
    public GameObject plaqueActive;
    public GameObject plaqueDesactive;

    public bool lockActivation;

    public bool teste;
    public bool other;

    //[HideInInspector]
    public bool activePlaqueBois;


    void Start()
    {
        lockActivation = false;
        other = false;
        teste = false;
        activePlaqueBois = false;
        plaqueActive.SetActive(false);
    }

    private void Update()
    {
        if (teste == true)
        {
            plaqueActive.SetActive(true);
            plaqueDesactive.SetActive(false);
            activePlaqueBois = true;
        }

        if (teste == false && lockActivation == false)
        {
            plaqueActive.SetActive(false);
            plaqueDesactive.SetActive(true);
            activePlaqueBois = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        teste = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        teste = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de :
/// - Gérer les activateur pour les puzzle
/// - De check si chaque activateur sont activé en même temps pour envoyé un signal à l'obj au quel est attaché ce script
/// </summary>

public class GestionActivateur : MonoBehaviour
{
    public List<GameObject> whoActivate = new List<GameObject>();

    public bool linkMoulinLeft;
    public bool linkMoulinRight;
    public bool linkMoulinBoth;

    public bool canActive;

    private int counter;
    private int maxCounter;

    void Start()
    {
        canActive = false;
    }

    void Update()
    {
        foreach (GameObject element in whoActivate)
        {
            if (element.gameObject.tag == "Clepsydre")
            {
                maxCounter += 1;
                if (element.gameObject.GetComponent<Clepsydre>().actifClepsydre == true)
                {
                    counter += 1;
                }
            }

            if (element.gameObject.tag == "Plaque Pierre")
            {
                maxCounter += 1;
                if (element.gameObject.GetComponent<PlaqueDePression>().activeTrap == true)
                {
                    counter += 1;
                }
            }

            if (element.gameObject.tag == "Bassin")
            {
                maxCounter += 1;
                if (element.gameObject.GetComponent<Bassin>().actifBassin == true)
                {
                    counter += 1;
                }
            }

            if (element.gameObject.tag == "Moulin")
            {
                if (linkMoulinLeft == true)
                {
                    maxCounter += 1;
                    if (element.gameObject.GetComponent<Moulin>().moulinOnGauche == true)
                    {
                        counter += 1;
                    }
                }

                if (linkMoulinRight == true)
                {
                    maxCounter += 1;
                    if (element.gameObject.GetComponent<Moulin>().moulinOnDroit == true)
                    {
                        counter += 1;
                    }
                }

                if (linkMoulinBoth == true)
                {
                    maxCounter += 1;
                    if (element.gameObject.GetComponent<Moulin>().moulinOnGauche == true || element.gameObject.GetComponent<Moulin>().moulinOnDroit == true)
                    {
                        counter += 1;
                    }
                }
            }
        }
        
        if (counter == maxCounter)
        {
            canActive = true;
        }
        else
        {
            counter = 0;
            maxCounter = 0;
        }

    }
}

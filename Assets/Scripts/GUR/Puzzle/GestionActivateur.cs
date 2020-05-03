﻿using System.Collections;
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
    #region Variable
    [Header ("Glisser les élement à lier")]
    public List<GameObject> whoActivate = new List<GameObject>();

    [Header ("Si il y à des moulins")]
    public bool linkMoulinLeft;
    public bool linkMoulinRight;
    public bool linkMoulinBoth;

    public bool lockAfterFinish;

    [HideInInspector]
    public bool canActive;

    private int counter;
    private int maxCounter;
    #endregion

    void Start()
    {
        canActive = false;
    }

    void Update()
    {
        ActivationDestection();
        Analyse();
    }


    void ActivationDestection()
    {
        foreach (GameObject element in whoActivate)
        {
            if (element.gameObject.tag == "Clepsydre")
            {
                maxCounter += 1;
                if (element.gameObject.GetComponent<Clepsydre>().actifClepsydre == true)
                {
                    counter += 1;

                    if (canActive == true && lockAfterFinish == true)
                    {
                        element.gameObject.GetComponent<Clepsydre>().lockClepsydre = true;
                    }
                }
            }

            if (element.gameObject.tag == "Plaque Pierre")
            {
                maxCounter += 1;
                if (element.gameObject.GetComponent<PlaqueDePressionPierre>().activePlaquePierre == true)
                {
                    counter += 1;

                    if (canActive == true && lockAfterFinish == true)
                    {
                        element.gameObject.GetComponent<PlaqueDePressionPierre>().stayActivate = true;
                    }
                }
            }

            if (element.gameObject.tag == "Plaque Bois")
            {
                maxCounter += 1;
                if (element.gameObject.GetComponent<PlaquePressionBois>().activePlaqueBois == true)
                {
                    counter += 1;

                    /*if (gameObject.GetComponent<SpawnCaissePlaque>().caisse != null)
                    {
                        element.gameObject.GetComponent<PlaquePressionBois>().lockActivation = true;
                    }
                    else if (gameObject.GetComponent<SpawnCaissePlaque>().caisse == null)
                    {
                        element.gameObject.GetComponent<PlaquePressionBois>().lockActivation = false;
                    }*/
                }
            }

            if (element.gameObject.tag == "Bassin")
            {
                maxCounter += 1;
                if (element.gameObject.GetComponent<Bassin>().actifBassin == true)
                {
                    counter += 1;

                    if (canActive == true && lockAfterFinish == true)
                    {
                        element.gameObject.GetComponent<Bassin>().lockBassin = true;
                    }
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

                        if (canActive == true && lockAfterFinish == true)
                        {
                            element.gameObject.GetComponent<Moulin>().lockMoulinLeft = true;
                        }
                    }
                }

                if (linkMoulinRight == true)
                {
                    maxCounter += 1;
                    if (element.gameObject.GetComponent<Moulin>().moulinOnDroit == true)
                    {
                        counter += 1;

                        if (canActive == true && lockAfterFinish == true)
                        {
                            element.gameObject.GetComponent<Moulin>().lockMoulinRight = true;
                        }
                    }
                }

                if (linkMoulinBoth == true)
                {
                    maxCounter += 1;
                    if (element.gameObject.GetComponent<Moulin>().moulinOnGauche == true || element.gameObject.GetComponent<Moulin>().moulinOnDroit == true)
                    {
                        counter += 1;

                        if (canActive == true && lockAfterFinish == true)
                        {
                            element.gameObject.GetComponent<Moulin>().lockMoulinLeft = true;
                            element.gameObject.GetComponent<Moulin>().lockMoulinRight = true;
                        }
                    }
                }
            }
        }


    } // Fonction qui detecte si les élements dans la liste sont activé ou pas

    void Analyse()
    {
        if (counter == maxCounter)
        {
            canActive = true;
        }
        else
        {
            canActive = false;
            counter = 0;
            maxCounter = 0;
        }
    } // Onction qui active une bool si tous les élements de a liste sont actif en même temps
}
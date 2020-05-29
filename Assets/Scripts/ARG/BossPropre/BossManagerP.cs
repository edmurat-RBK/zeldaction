﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManagerP : MonoBehaviour
{
    public static BossManagerP instance = null;
    private int phaseCounter = 0;
    private bool isVulnarable = false;
    private Collider2D bossCollider = null;
    public ClepsydreBoss[] allclepsydre;
    private Pattern1 pattern1;
    private Pattern2P pattern2;
    private Pattern3P pattern3;
    private bool clepsydreOn;
    public GameObject papaObsidian;
    public GameObject mamanObsidian;

    //mouvement clepsydre
    [SerializeField]
    private GameObject clepsydreL;
    [SerializeField]
    private GameObject clepsydreR;
    private Vector2 positionBaseL;
    private Vector2 positionBaseR;
    [SerializeField]
    private int hightWhenClepsydreOff;

    //tiles de sol au dessus de la lave
    [SerializeField]
    private GameObject tilesG;
    [SerializeField]
    private GameObject tilesD;

    [SerializeField]
    private GameObject porte;
    private Animator animPorte;

    private Animator anim;
    [SerializeField]
    private int numberOfScene = 6;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        clepsydreOn = true;


        animPorte = porte.GetComponent<Animator>();
        anim = GetComponent<Animator>();

        bossCollider = GetComponent<Collider2D>();
        pattern1 = GetComponent<Pattern1>();
        pattern2 = GetComponent<Pattern2P>();
        pattern3 = GetComponent<Pattern3P>();

        positionBaseL = clepsydreL.transform.position;
        positionBaseR = clepsydreR.transform.position;

        papaObsidian.SetActive(false);
        mamanObsidian.SetActive(false);
    }

    private void Update()
    {
        if (clepsydreOn)
        {
            clepsydreL.transform.position = Vector2.MoveTowards(clepsydreL.transform.position, new Vector2(positionBaseL.x, (positionBaseL.y)), 5 * Time.deltaTime);
            clepsydreR.transform.position = Vector2.MoveTowards(clepsydreR.transform.position, new Vector2(positionBaseR.x, (positionBaseR.y)), 5 * Time.deltaTime);
        }
        else
        {
            clepsydreL.transform.position = Vector2.MoveTowards(clepsydreL.transform.position, new Vector2(positionBaseL.x, (positionBaseL.y + hightWhenClepsydreOff)), 5 * Time.deltaTime);
            clepsydreR.transform.position = Vector2.MoveTowards(clepsydreR.transform.position, new Vector2(positionBaseR.x, (positionBaseR.y + hightWhenClepsydreOff)), 5 * Time.deltaTime);
        }
        
        if (clepsydreOn == true && isVulnarable == false)
        {
            int fullCounter = 0;
            foreach (ClepsydreBoss clepsydre in allclepsydre)
            {
                if (clepsydre.isFull)
                {
                    fullCounter++;
                }
            }
            if (fullCounter == 2)
            {
                animPorte.SetBool("IsOpen", true);
                MakeVulnerable();
            }
        }
    }

    public void IncreasePhase()
    {
        anim.SetBool("BossStun", true);
        anim.SetBool("BossVulnerable", false);
        animPorte.SetBool("IsOpen", false);

        FindObjectOfType<AudioManager>().Play("Boss hit");

        phaseCounter += 1;
        switch (phaseCounter)
        {
            case 1:
                LunchPhase2();
                break;
            case 2:LunchPhase3();
                pattern2.totem.enabled = false;
                break;
            case 3:
                anim.SetBool("BossDeath", true);

                FindObjectOfType<AudioManager>().Play("Boss death");
                FindObjectOfType<AudioManager>().Stop("Boss musique");

                StartCoroutine(CutsceneLaunch());  //cinématique (avec du délai)
                break;
            default:Debug.Log("out of range");
                break;
        }
    }

    [ContextMenu("MakeVulnerable")]
    public void MakeVulnerable()
    {
        anim.SetBool("BossVulnerable", true);
        anim.SetBool("BossFireBall", false);

        FindObjectOfType<AudioManager>().Play("Eteindre ennemi");

        switch (phaseCounter)
        {
            case 0:pattern1.StopAllCoroutines();

                break;
            case 1: pattern2.StopAllCoroutines();
                pattern2.totem.ForceReturn();
                pattern2.hardStop = true;

                break;
            case 2:
                pattern3.totem2.gameObject.GetComponentInChildren<Animator>().SetBool("Destroy", true);
                Destroy(pattern3.totem2.gameObject,1f);
                

                break;
            default:
                Debug.Log("out of range");
                break;
        }    
            
        isVulnarable = true;
        bossCollider.enabled = true;

  
    }

    [ContextMenu("LunchPhase1")]
    public void LunchPhase1()
    {

        DesactivateClepsydre();
        bossCollider.enabled = false;
        StartCoroutine(pattern1.InitialisePattern1());

    }

    public void LunchPhase2()
    {

        pattern2.totem.gameObject.SetActive(true);
        isVulnarable = false;
        DesactivateClepsydre();
        bossCollider.enabled = false;
        StartCoroutine(pattern2.InitialisePattern2());
        papaObsidian.SetActive(true);
        tilesG.SetActive(false);
    }

    public void LunchPhase3()
    {

        isVulnarable = false;
        bossCollider.enabled = false;
        StartCoroutine(pattern3.InitialisePattern3());
        mamanObsidian.SetActive(true);
        tilesD.SetActive(false);
        DesactivateClepsydre();
    }

    [ContextMenu("clepsydre activeé")]
    public void ActivateClepsydre()
    {
        StartCoroutine(PlayChaineSong());
        //clepsydre qui descend 
        clepsydreOn = true;
        foreach (ClepsydreBoss clepsydre in allclepsydre)
        {
            clepsydre.lockClepsydre = false;
            clepsydre.remplissage = 0;
            clepsydre.isFull = false;
        }


    }

    [ContextMenu("clepsydre desactiveé")]
    public void DesactivateClepsydre()
    {
        StartCoroutine(PlayChaineSong());

        clepsydreOn = false;
        foreach (ClepsydreBoss clepsydre in allclepsydre)
        {
            clepsydre.lockClepsydre = true;
            clepsydre.remplissage = 0;
            clepsydre.isFull = false;
        }
    }

    private IEnumerator CutsceneLaunch()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(numberOfScene);
    }


    private IEnumerator PlayChaineSong()
    {
        FindObjectOfType<AudioManager>().Play("Boss chaine");
        yield return new WaitForSeconds(2f);
        FindObjectOfType<AudioManager>().Stop("Boss chaine");
    }
}

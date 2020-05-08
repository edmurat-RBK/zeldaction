using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Animator anim;

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
        anim = GetComponent<Animator>();

        bossCollider = GetComponent<Collider2D>();
        pattern1 = GetComponent<Pattern1>();
        pattern2 = GetComponent<Pattern2P>();
        pattern3 = GetComponent<Pattern3P>();

    }

    private void Update()
    {
        
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
                MakeVulnerable();
            }
        }
    }

    public void IncreasePhase()
    {
        anim.SetBool("BossStun", true);
        anim.SetBool("BossVulnerable", false);
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
                //lancer l'animation de mort
                //cinématique (avec du délai)
                break;
            default:Debug.Log("out of range");
                break;
        }
    }

    [ContextMenu("MakeVulnerable")]
    public void MakeVulnerable()
    {
        anim.SetBool("BossVulnerable", true);
        switch (phaseCounter)
        {
            case 0:pattern1.StopAllCoroutines();

                break;
            case 1: pattern2.StopAllCoroutines();
                pattern2.totem.ForceReturn();
                pattern2.hardStop = true;

                break;
            case 2:Destroy(pattern3.totem2.gameObject);

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
        //idle du boss
        //plus vulnérable
        DesactivateClepsydre();
        bossCollider.enabled = false;
        StartCoroutine(pattern1.InitialisePattern1());

    }

    public void LunchPhase2()
    {
        //idle du boss
        //plus vulnérable
        pattern2.totem.gameObject.SetActive(true);
        isVulnarable = false;
        DesactivateClepsydre();
        bossCollider.enabled = false;
        StartCoroutine(pattern2.InitialisePattern2());
        papaObsidian.SetActive(true);
    }

    public void LunchPhase3()
    {
        //idle du boss
        //plus vulnérable
        isVulnarable = false;
        bossCollider.enabled = false;
        StartCoroutine(pattern3.InitialisePattern3());
        mamanObsidian.SetActive(true);
        DesactivateClepsydre();
    }

    [ContextMenu("clepsydre activeé")]
    public void ActivateClepsydre()
    {
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
        //animation clepsydres qui remontent
        clepsydreOn = false;
        foreach (ClepsydreBoss clepsydre in allclepsydre)
        {
            clepsydre.lockClepsydre = true;
            clepsydre.remplissage = 0;
            clepsydre.isFull = false;
        }
    }

}

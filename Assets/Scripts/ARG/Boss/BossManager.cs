using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Management;

public class BossManager : Singleton<BossManager>
{
    #region variable
    public int dammageCount;
    private Pattern3 pattern3;
    private Pattern2 pattern2;
    private Pattern1 pattern1;
    private int cooldownPattern;
    public int cooldownPatternMin;
    public int cooldownPatternMax;
    //public bool canLunchPattern2;
    public bool vulnerable;
    public bool canBeVulnerable;
    public BoxCollider2D colliderDammage;
    public bool plankMouvement;
    #endregion

    private void Awake()
    {
        MakeSingleton(true);
        pattern3 = GetComponent<Pattern3>();
        pattern2 = GetComponent<Pattern2>();
        pattern1 = GetComponent<Pattern1>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dammageCount = 1;
        StartCoroutine(CooldownPattern());
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeVulnerable == true)
        {
            colliderDammage.enabled = true;
        }
        else colliderDammage.enabled = false;

        if (vulnerable == true)
        {
            StopAllCoroutines();
        }
    }

    void BossChoice()
    {
        switch (dammageCount)
        {
            case 1:
                plankMouvement = true;
                pattern1.enabled = true;
                break;

            case 2:
                //cooldownPatternMin = 10;
                //cooldownPatternMax = 12;
                GetComponent<Pattern2>().SlamPoint();
                pattern2.enabled = true;
                pattern1.enabled = false;
                plankMouvement = true;
                break;

            case 3:
                pattern2.enabled = false;
                pattern3.enabled = true;
                plankMouvement = true;
                //pattern2.enabled = false;
                //GetComponent<Pattern3>().startPattern3();
                break;
        }
    }

    public void DammageBoss()
    {
        if (vulnerable == true)
        {
            dammageCount += 1;
            vulnerable = false;
            IsVulnerable();
            plankMouvement = false;
            //StartCoroutine(CooldownPattern());

            if (canBeVulnerable == true)
            {
                canBeVulnerable = false;
                CooldownFunction();
                //StartCoroutine(CooldownPattern());
            }
        }

            

    }

    private IEnumerator CooldownPattern()
    {
        cooldownPattern = Random.Range(cooldownPatternMin, cooldownPatternMax);
        yield return new WaitForSeconds(cooldownPattern);
        BossChoice();
        canBeVulnerable = true;
    }

    public void CooldownFunction()
    {
        BossChoice();
        canBeVulnerable = true;
        //StartCoroutine(CooldownPattern());
        //canLunchPattern2 = true;
    }

    public void IsVulnerable()
    {

        //if (vulnerable == false)
        //{
        //    pattern1.vulnerable = false;
        //    pattern2.vulnerable = false;
        //    pattern3.vulnerable = false;

        //}
        //else if (vulnerable == true)
        //{
        //    pattern1.vulnerable = true;
        //    pattern2.vulnerable = true;
        //    pattern3.vulnerable = true;
        //}



    }

}

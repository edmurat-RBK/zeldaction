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
    public bool canLunchPattern2;
    public bool vulnerable;
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
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    CooldownFunction();
        //}
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    vulnerable = true;
        //    IsVulnerable();
        //}

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
                GetComponent<Pattern1>().WaveOfFlame();
                break;

            case 2:
                GetComponent<Pattern2>().SlamPoint();
                break;

            case 3:
                pattern3.enabled = true;
                pattern2.enabled = false;
                pattern1.enabled = false;
                break;
        }
    }

    public void DammageBoss(int dammage)
    {
        dammageCount += 1;
        vulnerable = false;
        IsVulnerable();
    }

    private IEnumerator CooldownPattern()
    {
        Debug.Log("je lance un nouveau pattern");
        cooldownPattern = Random.Range(cooldownPatternMin, cooldownPatternMax);
        yield return new WaitForSeconds(cooldownPattern);
        BossChoice();
    }

    public void CooldownFunction()
    {
        StartCoroutine(CooldownPattern());
        canLunchPattern2 = true;
    }

    public void IsVulnerable()
    {
        if (vulnerable == false)
        {
            pattern1.vulnerable = false;
            pattern2.vulnerable = false;
            pattern3.vulnerable = false;
        }
        else if (vulnerable == true)
        {
            pattern1.vulnerable = true;
            pattern2.vulnerable = true;
            pattern3.vulnerable = true;
        }

    }

}

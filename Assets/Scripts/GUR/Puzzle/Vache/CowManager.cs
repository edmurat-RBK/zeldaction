using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class CowManager : Singleton<CowManager>
{
    public GameObject[] eachState;

    [HideInInspector]
    public int numOfCow;

    public Transform spawnTpCow;

    private bool lockCoroutine;
    void Start()
    {
        eachState[0].gameObject.SetActive(true);
        eachState[1].gameObject.SetActive(false);
        eachState[2].gameObject.SetActive(false);


        if (UpgradesManager.List["vache"] == true)
        {
            eachState[0].gameObject.SetActive(false);
            eachState[1].gameObject.SetActive(false);
            eachState[2].gameObject.SetActive(true);
        }
    }

    public void SwitchHitBox()
    {
        numOfCow += 1;
        spawnTpCow.position = new Vector3((spawnTpCow.position.x - 1.5f), spawnTpCow.position.y, spawnTpCow.position.z);

        if (numOfCow == 1)
        {
            eachState[0].gameObject.SetActive(false);
            eachState[1].gameObject.SetActive(true);
        }

        if (numOfCow == 2)
        {
            eachState[1].gameObject.SetActive(false);
            eachState[2].gameObject.SetActive(true);

            if (lockCoroutine == false)
            {
                StartCoroutine(CoolDownSave());
            }
        }
    }

    IEnumerator CoolDownSave()
    {
        lockCoroutine = true;
        yield return new WaitForSecondsRealtime(4f);
        gameObject.GetComponent<UpgradeObject>().Gotcha();
    }
}

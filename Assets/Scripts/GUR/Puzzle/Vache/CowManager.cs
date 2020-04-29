using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class CowManager : Singleton<CowManager>
{
    public GameObject[] eachState;

    public int numOfCow;

    public Transform spawnTpCow;

    void Start()
    {
        eachState[0].gameObject.SetActive(true);
        eachState[1].gameObject.SetActive(false);
        eachState[2].gameObject.SetActive(false);
    }

    
    void Update()
    {
        
    }


    public void SwitchHitBox()
    {
        numOfCow += 1;
        spawnTpCow.position = new Vector3(spawnTpCow.position.x, (spawnTpCow.position.y + 1), spawnTpCow.position.z);

        if (numOfCow == 1)
        {
            eachState[0].gameObject.SetActive(false);
            eachState[1].gameObject.SetActive(true);
        }

        if (numOfCow == 2)
        {
            eachState[1].gameObject.SetActive(false);
            eachState[2].gameObject.SetActive(true);
        }
    }
}

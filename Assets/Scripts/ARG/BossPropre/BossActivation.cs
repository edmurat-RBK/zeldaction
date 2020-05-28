using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{

    public GameObject boss;
    public GameObject pathBlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boss.SetActive(true);
        pathBlock.SetActive(true);
    }


}

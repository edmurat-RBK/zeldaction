using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultisteFuite : MonoBehaviour
{
    public GameObject cultisteFuite;


    private void Start()
    {
        cultisteFuite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && UpgradesManager.List["cultiste"] == false)
        {
            cultisteFuite.SetActive(true);
            GetComponent<UpgradeObject>().Gotcha();
        }
    }
}

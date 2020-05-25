using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class ChamnObtainBucket : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && UpgradesManager.List["as bucket"] == false)
        {
            if (Input.GetButtonDown("X"))
            {
                collision.GetComponent<PlayerManager>().getBucket = true;
                collision.GetComponent<PlayerManager>().obtainBucket();
                GetComponent<UpgradeObject>().Gotcha();
            }
        }
    }
}

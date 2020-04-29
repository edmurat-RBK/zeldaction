using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    private bool canActivate;
    private bool lockCow;

    void Start()
    {
        canActivate = false;
        lockCow = false;
    }

    
    void Update()
    {
        if (canActivate == true && lockCow == false)
        {
            if (Input.GetButton("B"))
            {
                gameObject.transform.position = CowManager.Instance.spawnTpCow.position;
                transform.GetChild(0).gameObject.SetActive(false);
                CowManager.Instance.SwitchHitBox();
                lockCow = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 31)
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 31)
        {
            canActivate = false;
        }
    }
}

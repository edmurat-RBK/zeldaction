using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class DebugTool : MonoBehaviour
{
    private bool isInvinsible = false;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isInvinsible == false)
            {

                isInvinsible = true;
                PlayerManager.Instance.playerInvulnerable = true;
            }
            else
            {
                isInvinsible = false;
                PlayerManager.Instance.playerInvulnerable = false;
            }
        }
    }
}

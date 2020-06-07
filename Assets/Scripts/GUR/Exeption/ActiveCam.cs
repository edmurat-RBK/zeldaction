using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ActiveCam : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cam.Priority += 5;
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

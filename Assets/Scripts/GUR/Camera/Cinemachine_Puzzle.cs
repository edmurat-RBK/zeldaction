using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinemachine_Puzzle : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    public float watchingTime;

    private bool canWatch;

    void Start()
    {
        canWatch = true;
    }

    
    void Update()
    {
        if (gameObject.GetComponent<GestionActivateur>().canActive == true && canWatch == true)
        {
            StartCoroutine(TransitionCam());
        }
    }

    IEnumerator TransitionCam()
    {
        canWatch = false;
        cam.Priority += 2;
        yield return new WaitForSeconds(watchingTime);
        cam.Priority -= 2;
    }
}

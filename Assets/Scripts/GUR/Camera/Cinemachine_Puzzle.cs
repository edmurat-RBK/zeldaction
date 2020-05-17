using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinemachine_Puzzle : MonoBehaviour
{
    public bool bassinDestructible;
    public bool vache;

    public CinemachineVirtualCamera cam;

    public float watchingTime;

    private bool canWatch;

    void Start()
    {
        canWatch = true;
    }

    
    void Update()
    {
        if (bassinDestructible == false && vache == false)
        {
            if (canWatch == true)
            {
                if (gameObject.GetComponent<GestionActivateur>().canActive == true)
                {
                    StartCoroutine(TransitionCam());
                }
            }
        }

        if (bassinDestructible == true)
        {
            if (canWatch == true)
            {
                if (gameObject.GetComponent<Bassin>().fullDestroy == true)
                {
                    StartCoroutine(TransitionCam());
                }
            }
        }

        if (vache == true)
        {
            if (canWatch == true)
            {
                if (gameObject.GetComponent<Cow>().lockCow == true)
                {
                    StartCoroutine(TransitionCam());
                }
            }
        }
    }

    IEnumerator TransitionCam()
    {
        canWatch = false;
        cam.Priority += 5;
        yield return new WaitForSeconds(watchingTime);
        cam.Priority -= 5;
    }
}

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

    public bool volcanCam;

    void Start()
    {
        canWatch = true;
    }

    
    void Update()
    {
        if (UpgradesManager.List["volcan"] == false)
        {
            if (bassinDestructible == false && vache == false)
            {
                if (canWatch == true && volcanCam == true)
                {
                    if (gameObject.GetComponent<GestionActivateur>().canActive == true)
                    {
                        Debug.Log("Volcan");
                        StartCoroutine(TransitionCam());
                    }
                }
            }
        }

        if (UpgradesManager.List["milieu"] == false && UpgradesManager.List["volcan"] == false)
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
        }

        if (UpgradesManager.List["vache"] == false)
        {
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
    }

    IEnumerator TransitionCam()
    {
        canWatch = false;
        cam.Priority += 5;
        yield return new WaitForSeconds(watchingTime);
        cam.Priority -= 5;
    }
}

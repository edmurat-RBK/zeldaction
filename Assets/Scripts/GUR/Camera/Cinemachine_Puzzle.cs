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

    public bool canWatch;

    public bool volcanCam;

    public bool donjonVolcanCam1;
    public bool donjonVolcanCam2;
    public bool donjonVolcanCam3;

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
                        Debug.Log("Milieu");
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
                        Debug.Log("Vache");
                        StartCoroutine(TransitionCam());
                    }
                }
            }
        }

        if (UpgradesManager.List["donjonLave1"] == false)
        {
            if (canWatch == true && donjonVolcanCam1 == true)
            {
                    Debug.Log("donjonVolcan1");
                if (gameObject.GetComponent<GestionActivateur>().canActive == true)
                {
                    StartCoroutine(TransitionCam());
                }
            }

        }

        if (UpgradesManager.List["donjonLave2"] == false)
        {
            if (bassinDestructible == false && vache == false)
            {
                if (canWatch == true && donjonVolcanCam2 == true)
                {
                    if (gameObject.GetComponent<GestionActivateur>().canActive == true)
                    {
                        Debug.Log("donjonVolcan2");
                        StartCoroutine(TransitionCam());
                    }
                }
            }
        }

        if (UpgradesManager.List["donjonLave3"] == false)
        {
            if (bassinDestructible == false && vache == false)
            {
                if (canWatch == true && donjonVolcanCam3 == true)
                {
                    if (gameObject.GetComponent<GestionActivateur>().canActive == true)
                    {
                        Debug.Log("donjonVolcan3");
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

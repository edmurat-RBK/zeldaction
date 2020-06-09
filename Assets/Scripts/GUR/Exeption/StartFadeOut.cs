using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFadeOut : MonoBehaviour
{
    public float timeForFade;

    private GameObject fadeScreen;

    void Start()
    {
        fadeScreen = GameObject.FindWithTag("Fade");
        FadeManager.Instance.FadeOut(fadeScreen, timeForFade);
    }

}

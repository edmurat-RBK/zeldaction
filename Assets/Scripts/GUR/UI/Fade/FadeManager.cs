using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Management;


public class FadeManager : Singleton<FadeManager>
{
    public void FadeOut(GameObject fade, float time)
    {
        fade.GetComponent<Image>().CrossFadeAlpha(0f, time, false);
    }


    public void FadeIn(GameObject fade, float time)
    {
        fade.GetComponent<Image>().CrossFadeAlpha(1f, time, false);
    }

}

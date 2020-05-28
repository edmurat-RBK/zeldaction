using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TesteFade : MonoBehaviour
{
    public Image blackScreen;
    public FadeManager fadeManager;

    private void Start()
    {
        
    }

    public void ActiveFadeIn()
    {
        fadeManager.FadeIn(blackScreen);
    }
}

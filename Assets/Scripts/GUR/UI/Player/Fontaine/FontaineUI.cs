using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.UI;

public class FontaineUI : MonoBehaviour
{
    public GameObject objectSlider;
    public Slider slider;

    private bool enable;

   
    void Start()
    {
        objectSlider.SetActive(false);

        if (PlayerManager.Instance.getBucket == true)
        {
            objectSlider.SetActive(true);
            slider.maxValue = PlayerManager.Instance.maxCooldownF;
            slider.value = PlayerManager.Instance.cooldownF;
        }
    }

    
    void Update()
    {
        if (PlayerManager.Instance.getBucket == true && enable == false)
        {
            enable = true;
            objectSlider.SetActive(true);
        }
        slider.value = PlayerManager.Instance.maxCooldownF - PlayerManager.Instance.cooldownF;
    }
}

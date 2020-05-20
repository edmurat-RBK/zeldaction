using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.UI;

public class FontaineUI : MonoBehaviour
{
    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = PlayerManager.Instance.maxCooldownF;
        slider.value = PlayerManager.Instance.cooldownF;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = PlayerManager.Instance.maxCooldownF - PlayerManager.Instance.cooldownF;
    }
}

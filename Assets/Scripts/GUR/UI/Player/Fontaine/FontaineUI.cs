using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.UI;

public class FontaineUI : MonoBehaviour
{
    public Sprite newBar;

    public Image healthBar;

    
    public GameObject objectSlider;
    public Image slider;

    private bool enable;

   
    void Start()
    {
        objectSlider.SetActive(false);

        if (PlayerManager.Instance.getBucket == true)
        {
            objectSlider.SetActive(true);
        }
    }

    
    void Update()
    {
        if (PlayerManager.Instance.getBucket == true && enable == false)
        {
            enable = true;
            objectSlider.SetActive(true);
            healthBar.sprite = newBar;
        }

        slider.fillAmount = (PlayerManager.Instance.maxCooldownF - PlayerManager.Instance.cooldownF) / PlayerManager.Instance.maxCooldownF;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class HealthBar : MonoBehaviour
{
    public GameObject[] hearts;
    public Sprite fullHeart;

    [HideInInspector]
    public bool lockCanTake;

    private void Start()
    {
        lockCanTake = false;
    }

    private void Update()
    {
        if (lockCanTake == false)
        {
            lockCanTake = true;
            hearts = GameObject.FindGameObjectsWithTag("Heart");
            HealthSysteme();
        }
    }

    public void HealthSysteme()
    {
        if (GetComponent<PlayerHealth>().health > GetComponent<PlayerHealth>().maximumHealth)
        {
            GetComponent<PlayerHealth>().health = GetComponent<PlayerHealth>().maximumHealth;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < GetComponent<PlayerHealth>().health)
            {
                hearts[i].gameObject.GetComponent<Animator>().SetBool("perte", false);
            }
            else
            {
                hearts[i].gameObject.GetComponent<Animator>().SetBool("perte", true);
            }


            if (i < GetComponent<PlayerHealth>().maximumHealth)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    IEnumerator CoolDownAnim() 
    {
        Debug.Log("je rentre");
        yield return new WaitForSeconds(1.4f);
        //stock.sprite = emptyHeart;
    }
}

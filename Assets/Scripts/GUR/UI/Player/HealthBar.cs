using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;
public class HealthBar : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Image stock;
    private bool canEmpty;

    private void Start()
    {
        canEmpty = true;
        HealthSysteme();
    }

    public void HealthSysteme()
    {
        Debug.Log("je rentre");
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
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
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

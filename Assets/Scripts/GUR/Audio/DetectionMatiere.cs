﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class DetectionMatiere : MonoBehaviour
{
    public enum matière
    {
        sable,
        terre,
        intérieur
    }

    public matière wichMatter;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (wichMatter == matière.sable && PlayerManager.Instance.onDirt == false && PlayerManager.Instance.onConcrete == false)
            {
                PlayerManager.Instance.onSand = true;
            }

            if (wichMatter == matière.terre && PlayerManager.Instance.onSand == false && PlayerManager.Instance.onConcrete == false)
            {
                PlayerManager.Instance.onDirt = true;
            }

            if (wichMatter == matière.intérieur && PlayerManager.Instance.onDirt == false && PlayerManager.Instance.onSand == false)
            {
                PlayerManager.Instance.onConcrete = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (wichMatter == matière.sable)
            {
                PlayerManager.Instance.onSand = false;
            }

            if (wichMatter == matière.terre)
            {
                PlayerManager.Instance.onDirt = false;
            }

            if (wichMatter == matière.intérieur)
            {
                PlayerManager.Instance.onConcrete = false;
            }
        }
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de : 
///  - Détruire les caisse qui touche cet objet
/// </summary>
public class CaisseDestructor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 28)
        {
            Object.Destroy(collision.gameObject);
        }
    }
}
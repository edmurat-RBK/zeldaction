using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDrop : MonoBehaviour
{
    public int chanceDrop;
    public GameObject potion;

    public void RamdomDrop()
    {
        int random = Random.Range(0, 100);

        if (random <= chanceDrop)
        {
            Instantiate(potion, transform.position, transform.rotation); 
        }
    }
}

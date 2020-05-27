using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDrop : MonoBehaviour
{
    public int chanceDrop;
    public GameObject potion;

    private bool canDrop;

    
    public void RamdomDrop()
    {
        if (canDrop == false)
        {
            canDrop = true;
            int random = Random.Range(0, 100);

            if (random <= chanceDrop)
            {
                Instantiate(potion, transform.position, transform.rotation);
            }
        }
    }
}

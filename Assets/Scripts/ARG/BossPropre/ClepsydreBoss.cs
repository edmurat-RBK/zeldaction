using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClepsydreBoss : Clepsydre
{
    public bool isFull;

    protected override void Vidage()
    {
        if (remplissage < maxStockage)
        {
            base.Vidage();
        }
        else isFull = true;
            
    }
}

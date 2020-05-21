using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDoorOpen : MonoBehaviour
{
    public enum lieu
    {
        DonjonChaman,
        Milieu,
        Volcan,
        DonjonLave
    }

    public lieu where;

    void Start()
    {
        if (where == lieu.DonjonChaman)
        {
            if (UpgradesManager.List["finishShaman"] == true)
            {
                GetComponent<GestionActivateur>().whoActivate.RemoveAll(list_item => list_item != null);
            }
        }

        if (where == lieu.Milieu)
        {
            if (UpgradesManager.List["milieu"] == true)
            {
                GetComponent<GestionActivateur>().whoActivate.RemoveAll(list_item => list_item != null);
            }
        }

        if (where == lieu.Volcan)
        {
            if (UpgradesManager.List["volcan"] == true)
            {
                GetComponent<GestionActivateur>().whoActivate.RemoveAll(list_item => list_item != null);
            }
        }
    }
}

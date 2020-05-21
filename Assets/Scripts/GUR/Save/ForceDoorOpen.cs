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
        DonjonLave1,
        DonjonLave2,
        DonjonLave3
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

        if (where == lieu.DonjonLave1)
        {
            if (UpgradesManager.List["donjonLave1"] == true)
            {
                GetComponent<GestionActivateur>().whoActivate.RemoveAll(list_item => list_item != null);
            }
        }

        if (where == lieu.DonjonLave2)
        {
            if (UpgradesManager.List["donjonLave2"] == true)
            {
                GetComponent<GestionActivateur>().whoActivate.RemoveAll(list_item => list_item != null);
            }
        }

        if (where == lieu.DonjonLave3)
        {
            if (UpgradesManager.List["donjonLave3"] == true)
            {
                GetComponent<GestionActivateur>().whoActivate.RemoveAll(list_item => list_item != null);
            }
        }
    }
}

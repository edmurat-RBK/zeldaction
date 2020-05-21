using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDoorOpen : MonoBehaviour
{
    public enum lieu
    {
        DonjonChaman,
        Vache,
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
    }
}

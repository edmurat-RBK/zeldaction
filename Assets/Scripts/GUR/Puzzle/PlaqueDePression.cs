using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueDePression : MonoBehaviour
{
    public bool activeTrap = false;

    public GameObject plaqueActive;
    public GameObject plaqueDesactive;

    void Start()
    {
        plaqueActive.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        plaqueActive.SetActive(true);
        plaqueDesactive.SetActive(false);
        activeTrap = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        plaqueActive.SetActive(false);
        plaqueDesactive.SetActive(true);
        activeTrap = false;
    }
}

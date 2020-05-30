using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneActivation : MonoBehaviour
{
    public GameObject[] objectsToActivate;

    public bool activeObject;
    private void Start()
    {
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(false);
        }
    }


    private void Update()
    {
        if (activeObject == true)
        {
            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                objectsToActivate[i].SetActive(true);
            }
        }
        else if (activeObject == false)
        {
            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                objectsToActivate[i].SetActive(false);
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            activeObject = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            activeObject = false;
        }
    }
}

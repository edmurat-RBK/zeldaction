using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertChef : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (UpgradesManager.List["chef"] == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && UpgradesManager.List["chef"] == false)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            GetComponent<UpgradeObject>().Gotcha();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButton("X"))
            {
                GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }

    }
}

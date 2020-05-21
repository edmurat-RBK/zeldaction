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
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<UpgradeObject>().Gotcha();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButton("X"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

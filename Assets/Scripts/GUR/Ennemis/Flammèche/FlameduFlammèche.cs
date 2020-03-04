using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameduFlammèche : MonoBehaviour
{
    public float timesForDestruction;

    void Start()
    {
        StartCoroutine(TimeBeforeDestruction());
    }
    IEnumerator TimeBeforeDestruction()
    {
        yield return new WaitForSeconds(timesForDestruction);
        Destroy(gameObject);
    }
}

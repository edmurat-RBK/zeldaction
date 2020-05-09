using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad instance = null;

    private void Awake()
{
    if (instance == null)
    {
        instance = this;
       DontDestroyOnLoad(gameObject);
    }

    else Destroy(gameObject);
}
}

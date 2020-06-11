using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletPlayerPref : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey("F2MV2018New");
    }
}

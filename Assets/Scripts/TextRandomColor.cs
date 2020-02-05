using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRandomColor : MonoBehaviour
{
    public int red;
    public int green;
    public int blue;

    private int redDirection = 1;
    private int greenDirection = 1;
    private int blueDirection = 1;

    private Text textComponent;


    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 500) == 0 || red == 0 || red == 255)
        {
            redDirection *= -1;
        }
        if (Random.Range(0, 500) == 0 || green == 0 || green == 255)
        {
            greenDirection *= -1;
        }
        if (Random.Range(0, 500) == 0 || blue == 0 || blue == 255)
        {
            blueDirection *= -1;
        }

        red += redDirection;
        green += greenDirection;
        blue += blueDirection;

        textComponent.color = new Color(red / 255.0f, green / 255.0f, blue / 255.0f);
    }
}

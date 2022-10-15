using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// class for the 5 value slider

public class sliderMapToText : MonoBehaviour
{
    public Panel panel;
    public Slider slider;
    private double v0 = 1000;
    private double v1 = 2000;
    private double v2 = 3000;
    private double v3 = 4000;
    private double v4 = 5000;
    private double mappedValue;
    private Text textSliderValue;


    // Start is called before the first frame update
    void Start()
    {

        textSliderValue = GetComponent<Text>();
        ShowSliderValue();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // here you edit the default values of R and C
    public void SetValue(double value)
    {
        if (panel.CircuitElementType == 1)
        {
            v0 = 510;
            v1 = 1000;
            v2 = 7500;
            v3 = 39000;
            v4 = 75000;
        }
        else if (panel.CircuitElementType == 3)
        {
            v0 = 1e-09;
            v1 = 1e-08;
            v2 = 100e-09;
            v3 = 1e-06;
            v4 = 10e-06;
        }

       
        if(value == v0)
        {
            slider.value = 0;
        }
        else if (value == v1)
        {
            slider.value = 1;
        }
        else if (value == v2)
        {
            slider.value = 2;
        }
        else if (value == v3)
        {
            slider.value = 3;
        }
        else if (value == v4)
        {
            slider.value = 4;
        }
        else
        {

        }
    }

    public void ShowSliderValue()
    {
        
        switch (slider.value)
        {
            case 0:
                mappedValue = v0;
                break;
            case 1:
                mappedValue = v1;
                break;
            case 2:
                mappedValue = v2;
                break;
            case 3:
                mappedValue = v3;
                break;
            case 4:
                mappedValue = v4;
                break;
            default:
                mappedValue = v0;
                break;

        }
        if (panel.CircuitElementType == 1)
        {
            GetComponent<Text>().text = mappedValue + "\u03A9";
        }
        else if (panel.CircuitElementType == 3)
        {
            GetComponent<Text>().text = mappedValue*1e+6 + "mF";
        }
        
       
    }

    
    public double getMappedValue()
    {
        return mappedValue;
    }
}

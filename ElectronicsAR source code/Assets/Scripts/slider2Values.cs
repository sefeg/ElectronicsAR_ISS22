using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Slider class for two values
public class slider2Values : MonoBehaviour
{
    public Panel panel;
    public Slider slider;
    private string v0 = "PNP";
    private string v1 = "NPN";

    private string mappedValue;
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

    // Set value of the slider 
    // Here change the default values
    public void SetValue(int value)
    {

        if (panel.CircuitElementType == 3)
        {
            v0 = "OFF";
            v1 = "Loaded";

        }
        else if (panel.CircuitElementType == 4)
        {
            v0 = "PNP";
            v1 = "NPN";

        }

        if (value == 0)
        {
            slider.value = 0;
        }
        else if (value == 1)
        {
            slider.value = 1;
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
            default:
                mappedValue = v0;
                break;

        }

            GetComponent<Text>().text = mappedValue;




    }

    public int getMappedValue()
    {
        return (int)slider.value;
    }
}

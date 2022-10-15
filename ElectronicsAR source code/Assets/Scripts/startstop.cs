using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Class to change the button value if the simulation is currently running or not
// Currently is doesnt work correkt cause of a unity bug
public class startstop : MonoBehaviour
{
    public ElementPositionController controller;
    public bool isStarted = false;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void pressed()
    {
        if (isStarted)
        {
            controller.stopAnalysis = true;

        }
        else
        {
            Debug.Log("setTrue");
            isStarted = true;
            text.text = "Stop";
            // MOCK FOR STUDY
            //text.text = "Start";
            controller.startAnalyse();
        }
    }

    public void setIsStartedFalse()
    {
        Debug.Log("setFalse");
        isStarted = false;
        //text.text = "Stop";
        text.text = "Start";
        
    }

}

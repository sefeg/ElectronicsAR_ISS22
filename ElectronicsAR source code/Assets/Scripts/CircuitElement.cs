using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Class for all types of CircuitElement
public class CircuitElement : MonoBehaviour
{
    public int id;
    public string elementName;
    public double value = 1.0e3;
    public double voltage;
    public double current;
    
    public int type = 0;
    public GameObject pol1;
    public GameObject pol2;
    public GameObject pol3;
    public Text text;
    public currentAnim currentAnim;
    public currentAnim currentAnimB;
    public currentAnim currentAnimC;
    public Renderer body;
    public GameObject arrow;

    public double vbe;
    public double ic;
    public double vbc;
    public double ib;

    public int npn;
    public int precondition;


    private double nocurrent = 1e-10;
    private double ledIOn = 1e-3;
    private bool voltagePosChange = true;
    private bool voltageNegChange = false;


    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    // refresh shown information each frame
    void Update()
    {
       
        refreshElemt();

        if (type == 2 && SceneManager.GetActiveScene().name == "Analyser")
        {
            refreshLight();
        }
        
    }

    // Check if current for light LED is sufficient
    private void refreshLight()
    {
        if (current >= ledIOn)
        {
           
            body.material.EnableKeyword("_EMISSION");
        }
        else
        {
           
            body.material.DisableKeyword("_EMISSION");
        }
    }

    // refresh the currents and voltages of this element
    void refreshElemt()
    {

        // If Resistor, turn voltage arrow according to voltage
        if(arrow != null && type == 1)
        {
            

            if(voltage >= 0 && voltagePosChange == false)
            {
                arrow.transform.Rotate(0, 0, 180, Space.Self);
                voltagePosChange = true;
                voltageNegChange = false;
            }
            if(voltage<0 && voltageNegChange == false)
            {
                arrow.transform.Rotate(0, 0, 180, Space.Self);
                voltagePosChange = false;
                voltageNegChange = true;
            }
        }

        if (text != null)
        {
           
            if (type==4)
            {
                text.text = "Vbe: " + vbe.ToString("F3") + "V" + "\n" + "vbc: " + vbc.ToString("F3") + "V" + "\n" 
                           + "ic: " + (ic*1000).ToString("F3") + "mA" + "\n" + "ib: " + (ib*1000).ToString("F3") + "mA";
            }
            else if(type == 1)
            {
                text.text = Math.Abs(voltage).ToString("F3") + "V" + "\n" + Math.Abs(current*1000).ToString("F3") + "mA";
                
            }
            else
            {
                text.text = voltage.ToString("F3") + "V" + "\n" + (current*1000).ToString("F3") + "mA";

            }
        }
        
       
        // adjust current Animation to new values if necessary
        if (currentAnim != null)
        {

            if (type == 4)
            {
                if (-ic - ib > -nocurrent && -ic - ib < nocurrent)
                {
                    currentAnim.stopAnim();
                }
                else if (-ic-ib < -nocurrent)
                {
                    currentAnim.startAnimForward();
                }
                else if (-ic - ib > nocurrent)
                {
                    currentAnim.startAnimBw();
                }


                if (ib > -nocurrent && ib < nocurrent)
                {
                    currentAnimB.stopAnim();
                }
                else if (ib < nocurrent)
                {
                    currentAnimB.startAnimForward();
                }
                else if (ib > nocurrent)
                {
                    currentAnimB.startAnimBw();
                }


                if (ic> -nocurrent && ic< nocurrent)
                {
                    currentAnimC.stopAnim();
                }
                else if (ic < nocurrent)
                {
                    currentAnimC.startAnimForward();
                }
                else if (ic > nocurrent)
                {
                    currentAnimC.startAnimBw();
                }
            }
            else
            {
                if (current < nocurrent && current> -nocurrent)
                {
                    currentAnim.stopAnim();
                }
                else if (current > nocurrent)
                {
                    currentAnim.startAnimForward();
                }
                else if (current < -nocurrent)
                {
                    currentAnim.startAnimBw();
                }
            }
          
        }

    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Options GUI
public class analyOptions : MonoBehaviour
{
    public GameObject TextPanel;
    public GameObject scrollableList;
    List<GameObject> clones = new List<GameObject>();
    ElementPositionController c;
    public GameObject ui;
    public GameObject analyser;

    public static bool currentAnimation = false;
    // Start is called before the first frame update
    void Start()
    {
        c = analyser.GetComponent<ElementPositionController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Open GUI
    public void Open()
    {

        if (ui.activeSelf)
        {
            return;
        }

        clones.Clear();
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
           
            
            GameObject tmp;
            // analysing Speed
            tmp = Instantiate(TextPanel);
            tmp.transform.GetChild(1).gameObject.SetActive(true);
            tmp.transform.SetParent(scrollableList.transform, false);
            clones.Add(tmp);
            tmp.transform.GetChild(0).GetComponent<Text>().text = "Analysis Speed";
            tmp.transform.GetChild(1).GetComponent<InputField>().text = c.analyseSpeed.ToString();

            // Animation On/ off
            tmp = Instantiate(TextPanel);
            tmp.transform.GetChild(2).gameObject.SetActive(true);
            tmp.transform.SetParent(scrollableList.transform, false);
            clones.Add(tmp);
            tmp.transform.GetChild(0).GetComponent<Text>().text = "Current Animation";
            tmp.transform.GetChild(2).GetComponent<Toggle>().isOn = currentAnimation;


            Time.timeScale = 0f;
        }
    }
    public void Close()
    {
        ui.SetActive(!ui.activeSelf);

        // Write back values
        c.analyseSpeed = Convert.ToDouble(clones[0].transform.GetChild(1).GetComponent<InputField>().text);
        currentAnimation = clones[1].transform.GetChild(2).GetComponent<Toggle>().isOn;
        Destroy(clones[0]);
        Destroy(clones[1]);
        clones.Clear();



        clones.Clear();
        if (!ui.activeSelf)
        {
            Time.timeScale = 1f;
        }
    }
}

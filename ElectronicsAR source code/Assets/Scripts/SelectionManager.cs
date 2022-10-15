using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to open UI of the elements
public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string clickableTag = "clickable";
    public GameObject TextPanel;
    public GameObject scrollableList;
    List<GameObject> clones = new List<GameObject>();
    CircuitElement c;

    public GameObject ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    checkTouch(Input.GetTouch(0).position);
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                checkTouch(Input.mousePosition);
            }
        }


        
    }

    // Raycast from camera
    private void checkTouch(Vector3 pos)
    {
        var ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
                {
                    var selection = hit.transform;
                    Debug.Log(selection.name);
                    if (selection.CompareTag(clickableTag))
                    {
                        while (selection.GetComponent<CircuitElement>() == null)
                        {
                            selection = selection.parent;
                        }
                
                        Open(selection.gameObject);
                    }
            
                }
    }


    // Depended of the hit element, creates different fields for information
    public void Open(GameObject obj)
    {

        if (ui.activeSelf)
        {
            return;
        }

        clones.Clear();
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            c = obj.GetComponent<CircuitElement>();
            Debug.Log(c.name);
            GameObject tmp;
            tmp = Instantiate(TextPanel);
            tmp.transform.SetParent(scrollableList.transform, false);
            clones.Add(tmp);
            tmp.transform.GetChild(0).GetComponent<Text>().text = "Name";
            tmp.transform.GetChild(1).gameObject.SetActive(true);
            tmp.transform.GetChild(1).GetComponent<InputField>().text = c.elementName;
            if(c.type == 1)
            {
                tmp = Instantiate(TextPanel);
                tmp.transform.SetParent(scrollableList.transform, false);
                clones.Add(tmp);
                tmp.transform.GetChild(0).GetComponent<Text>().text = "Resistor";
                tmp.transform.GetChild(3).gameObject.SetActive(true);
                tmp.GetComponent<Panel>().SetType(c.type);
                tmp.transform.GetChild(3).GetChild(0).GetComponent<sliderMapToText>().SetValue(c.value);

            }
            else if (c.type == 3)
            {
                tmp = Instantiate(TextPanel);
                tmp.transform.SetParent(scrollableList.transform, false);
                clones.Add(tmp);
                tmp.transform.GetChild(0).GetComponent<Text>().text = "Capacity";
                tmp.transform.GetChild(3).gameObject.SetActive(true);
                tmp.GetComponent<Panel>().SetType(c.type);
                tmp.transform.GetChild(3).GetChild(0).GetComponent<sliderMapToText>().SetValue(c.value);

                
                tmp = Instantiate(TextPanel);
                tmp.transform.SetParent(scrollableList.transform, false);
                clones.Add(tmp);
                tmp.transform.GetChild(0).GetComponent<Text>().text = "Precondition";
                tmp.transform.GetChild(4).gameObject.SetActive(true);
                tmp.GetComponent<Panel>().SetType(c.type);
                tmp.transform.GetChild(4).GetChild(0).GetComponent<slider2Values>().SetValue(c.precondition);
                

            }
            else if (c.type ==4)
            {
                tmp = Instantiate(TextPanel);
                tmp.transform.SetParent(scrollableList.transform, false);
                clones.Add(tmp);
                tmp.transform.GetChild(0).GetComponent<Text>().text = "";
                tmp.transform.GetChild(4).gameObject.SetActive(true);
                tmp.GetComponent<Panel>().SetType(c.type);
                tmp.transform.GetChild(4).GetChild(0).GetComponent<slider2Values>().SetValue(c.npn);
            }


            //DEBUG
            /*c = obj.GetComponent<CircuitElement>();
            
            tmp = Instantiate(TextPanel);
            tmp.transform.SetParent(scrollableList.transform, false);
            clones.Add(tmp);
            tmp.transform.GetChild(0).GetComponent<Text>().text = "Pol1 (DEBUG)";
            tmp.transform.GetChild(1).gameObject.SetActive(true);
            tmp.transform.GetChild(1).GetComponent<InputField>().text = c.pol1.name;
            tmp = Instantiate(TextPanel);
            tmp.transform.SetParent(scrollableList.transform, false);
            clones.Add(tmp);
            tmp.transform.GetChild(0).GetComponent<Text>().text = "Pol2 (DEBUG)";
            tmp.transform.GetChild(1).gameObject.SetActive(true);
            tmp.transform.GetChild(1).GetComponent<InputField>().text = c.pol2.name;
            */
            // DEBUG


            //   if (!string.IsNullOrEmpty(name))
            //  {
            //        Text textObject = ui.gameObject.GetComponentInChildren<Text>();
            //       textObject.text = name;
            //    }
            //Time.timeScale = 0f;
        }
    }

    // Methode to close UI
    // Write back the data to variables
    public void Close()
    {
        ui.SetActive(!ui.activeSelf);
       // foreach (GameObject clone in clones)
       // {
            c.elementName = clones[0].transform.GetChild(1).GetComponent<InputField>().text;
            Destroy(clones[0]);
        if(c.type == 1)
        {
            c.value = clones[1].transform.GetChild(3).GetChild(0).GetComponent<sliderMapToText>().getMappedValue();
            //c.value = Convert.ToDouble (clones[1].transform.GetChild(1).GetComponent<InputField>().text);
            Destroy(clones[1]);
        }
        else if (c.type == 3)
        {
            c.value = clones[1].transform.GetChild(3).GetChild(0).GetComponent<sliderMapToText>().getMappedValue();
            //c.value = Convert.ToDouble(clones[2].transform.GetChild(1).GetComponent<InputField>().text);
            Destroy(clones[1]);
            c.precondition = clones[2].transform.GetChild(4).GetChild(0).GetComponent<slider2Values>().getMappedValue();
            Destroy(clones[2]);
        }
        else if (c.type == 4)
        {
            c.npn = clones[1].transform.GetChild(4).GetChild(0).GetComponent<slider2Values>().getMappedValue();
            Destroy(clones[1]);
        }

        // }

        //DEBUG
        foreach (var item in clones)
        {
            Destroy(item);
        }
        // DEBUG
        clones.Clear();
        if (!ui.activeSelf)
        {
            //Time.timeScale = 1f;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;

[Obsolete]
public class SpiceController : MonoBehaviour
{
    List<GameObject> objects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAnalyse()
    {
        var diodenModel = new DiodeModel("ISimulation");
        var ckt = new Circuit(diodenModel, new VoltageSource("V1", "node1", "0", 0.0));

        List<String> usedNodesName = new List<string>();
        List<GameObject> usedNodes = new List<GameObject>();

        

        CircuitElement tmp;
        foreach (GameObject obj in objects)
        {
            tmp = obj.GetComponent<CircuitElement>();
            if (tmp.type == 1)
            { ckt.Add(new Resistor(tmp.elementName, tmp.pol1.name, tmp.pol2.name, tmp.value));
                // doppelte möglicht??? listeLeeren??? TODO
                usedNodes.Add(tmp.pol1);
                usedNodes.Add(tmp.pol2);

                Debug.Log("neuer resistor");
            }else if (tmp.type == 2)
            {
                ckt.Add(new Diode(tmp.elementName, tmp.pol1.name, tmp.pol2.name, diodenModel.Name));
                usedNodes.Add(tmp.pol1);
                usedNodes.Add(tmp.pol2);
            }
        }

        var dc = new DC("dc", "V1", 5.0, 5.0, 0.001);

        dc.ExportSimulationData += (sender, args) =>
        {
            foreach (GameObject node in usedNodes)
            {
                
                node.GetComponent<Node>().voltage = args.GetVoltage(node.name); 
            }
            //var input = args.GetVoltage("in");
            // var output = args.GetVoltage("out");
            //Debug.Log(args.GetVoltage("input") + args.GetVoltage("0"));
        };

        dc.Run(ckt);

        foreach (GameObject obj in objects)
        {
            tmp = obj.GetComponent<CircuitElement>();
            Debug.Log(tmp.pol1.GetComponent<Node>().voltage);
            Debug.Log(tmp.pol2.GetComponent<Node>().voltage);
            tmp.text.text = (tmp.pol1.GetComponent<Node>().voltage - tmp.pol2.GetComponent<Node>().voltage).ToString() + "V";
        }

    }
           


    public void registerObject(GameObject obj)
    {
        objects.Add(obj);
    }

    public void deregisterObject(GameObject obj)
    {
        objects.Remove(obj);
    }
}

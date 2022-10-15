using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using UnityEngine.SceneManagement;

// Class for analysing the Circuit 
public class ElementPositionController : MonoBehaviour
{

    public elemtPositions elemtPositions;
    public GameObject parent;
    public GameObject circuitElementLEDPrefab;
    public GameObject circuitElementResPrefab;
    public GameObject circuitElementCapacitorPrefab;
    public GameObject circuitElementBJTPrefab;
    public GameObject circuitElementWirePrefab;
    public GameObject circuitElementWireExtraBigPrefab;
    public GameObject circuitElementWireExtraSmallPrefab;

    // set analysing Speed, doesnt work correct
    public double analyseSpeed = 1;

    public bool stopAnalysis = false;
    public startstop startstop;

    // 0 DC
    // 1 Transient
    // not used at the moment
    public int simType = 1;


    List<GameObject> objects = new List<GameObject>();
    List<CircuitElement> cElements = new List<CircuitElement>();


    List<RealPropertyExport> currentExports = new List<RealPropertyExport>();

    // Start is called before the first frame update
    // The detected elements from last scene are instantiated
    void Start()
    {   
        Debug.Log(elemtPositions.listOfFoundElements.Count);
        //Debug.Log(elemtPositions.listOfFoundElements[0].Item3.type);
        GameObject tmp;
        
        foreach (var (pos, rot, elem) in elemtPositions.listOfFoundElements)
        {
            if(elem.type == 1)
            {
                Debug.Log("new Resistor");
                tmp = Instantiate(circuitElementResPrefab, pos, rot);
                tmp.transform.SetParent(parent.transform, false);
                objects.Add(tmp);
                cElements.Add(tmp.GetComponent<CircuitElement>());
                elemtPositions.pasteCircuitElement(elem, tmp.GetComponent<CircuitElement>());
                
                
                
            }else if(elem.type == 2)
            {
                tmp = Instantiate(circuitElementLEDPrefab, pos, rot);
                tmp.transform.SetParent(parent.transform, false);
                objects.Add(tmp);
                cElements.Add(tmp.GetComponent<CircuitElement>());
                elemtPositions.pasteCircuitElement(elem, tmp.GetComponent<CircuitElement>());

            }
            else if (elem.type == 3)
            {
                tmp = Instantiate(circuitElementCapacitorPrefab, pos, rot);
                tmp.transform.SetParent(parent.transform, false);
                objects.Add(tmp);
                cElements.Add(tmp.GetComponent<CircuitElement>());
                elemtPositions.pasteCircuitElement(elem, tmp.GetComponent<CircuitElement>());

            }
            else if (elem.type == 4)
            {
                tmp = Instantiate(circuitElementBJTPrefab, pos, rot);
                tmp.transform.SetParent(parent.transform, false);
                objects.Add(tmp);
                cElements.Add(tmp.GetComponent<CircuitElement>());
                elemtPositions.pasteCircuitElement(elem, tmp.GetComponent<CircuitElement>());

            }
            else if (elem.type == 0)
            {
                tmp = Instantiate(circuitElementWirePrefab, pos, rot);
                tmp.transform.SetParent(parent.transform, false);
                objects.Add(tmp);
                cElements.Add(tmp.GetComponent<CircuitElement>());
                elemtPositions.pasteCircuitElement(elem, tmp.GetComponent<CircuitElement>());
            }
            else if (elem.type == -1)
            {
                tmp = Instantiate(circuitElementWireExtraBigPrefab, pos, rot);
                tmp.transform.SetParent(parent.transform, false);
                objects.Add(tmp);
                cElements.Add(tmp.GetComponent<CircuitElement>());
                elemtPositions.pasteCircuitElement(elem, tmp.GetComponent<CircuitElement>());
            }
            else if (elem.type == -2)
            {
                tmp = Instantiate(circuitElementWireExtraSmallPrefab, pos, rot);
                tmp.transform.SetParent(parent.transform, false);
                objects.Add(tmp);
                cElements.Add(tmp.GetComponent<CircuitElement>());
                elemtPositions.pasteCircuitElement(elem, tmp.GetComponent<CircuitElement>());
            }



        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    
    public void changeSceneToRec()
    {
        elemtPositions.writeBack(cElements);
        SceneManager.LoadScene("Recognition");
    }


    // Called with the "start" Button
    // corresponding circuit are build in SpiceSharp
    public void startAnalyse()
    {
        // Defined models for LED and BJT 
        var diodenModel = new DiodeModel("LEDModel");
        diodenModel.SetParameter("n", 1.8)
            .SetParameter("rs", 3.3);
        var bjtModelNPN = new BipolarJunctionTransistorModel("bjtModel");
        bjtModelNPN.SetParameter("npn", true)
           .SetParameter("bf", 200.0)
           .SetParameter("cjc", 20e-12)
           .SetParameter("cje", 20e-12)
           .SetParameter("is", 1e-16);
        var bjtModelPNP = new BipolarJunctionTransistorModel("bjtModelPNP");
        bjtModelPNP.SetParameter("pnp", true)
           .SetParameter("bf", 200.0)
           .SetParameter("cjc", 20e-12)
           .SetParameter("cje", 20e-12)
           .SetParameter("is", 1e-16);


        var ckt = new Circuit(diodenModel, bjtModelNPN, bjtModelPNP, new VoltageSource("V1", "Node33", "0", 9));

        ckt.Add(new VoltageSource("node34","Node34" , "Node33", 0));
        ckt.Add(new VoltageSource("node1", "Node2", "0", 0));

        List<String> usedNodesName = new List<string>();
        List<GameObject> usedNodes = new List<GameObject>();
        //  List<GameObject> circuitElements = new List<GameObject>();



        // TODO
        if(simType == 0)
        {
            //var sim = new DC("dc", "V1", 9.0, 9.0, 0.00);
           // var sim = new Transient("sim", 0.0001, 1);
        }
        else //if (simType == 1)
        {
            
        }
        
        var sim = new Transient("sim", 0.0001, 100);


        currentExports.Clear();
        // elements are added to circuit
        try
        {
            foreach (CircuitElement celem in cElements)
            {

                // tmp = obj.GetComponent<CircuitElement>();
                Debug.Log("analyse start");
                if (celem.type == 1)
                {
                    Debug.Log("neuer resistor 0");
                    ckt.Add(new Resistor(celem.id.ToString(), celem.pol1.name, celem.pol2.name, celem.value));

                    Debug.Log("neuer resistor pol1 : " + celem.pol1.name + " pol2: " + celem.pol2.name);

                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "i"));
                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "v"));

                    Debug.Log("neuer resistor");
                }
                else if (celem.type == 2)
                {
                    Debug.Log("neue diode");
                    Debug.Log(celem.id.ToString());
                    Debug.Log(celem.pol2.name);
                    Debug.Log(diodenModel.Name);
                    ckt.Add(new Diode(celem.id.ToString(), celem.pol2.name, celem.pol1.name, diodenModel.Name));
                    Debug.Log(celem.elementName);
                    usedNodes.Add(celem.pol1);
                    usedNodes.Add(celem.pol2);
                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "i"));
                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "v"));
                    Debug.Log("neue diode");
                }
                else if (celem.type == 3)
                {

                    ckt.Add(new Capacitor(celem.id.ToString(), celem.pol2.name, celem.pol1.name, celem.value));
                    if (celem.precondition == 1)
                    {
                       
                        sim.TimeParameters.InitialConditions[celem.pol2.name] = 12;
                        sim.TimeParameters.InitialConditions[celem.pol1.name] = 1;
                    }
                    else
                    {
                        sim.TimeParameters.InitialConditions[celem.pol2.name] = 0;
                        sim.TimeParameters.InitialConditions[celem.pol1.name] = -1;
                    }

                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "i"));
                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "v"));

                }
                else if (celem.type == 4)
                {

                    if (celem.npn == 1)
                    {
                        ckt.Add(new BipolarJunctionTransistor(celem.id.ToString(), celem.pol2.name, celem.pol1.name, celem.pol3.name, "0", bjtModelNPN.Name));
                    }
                    else
                    {
                        ckt.Add(new BipolarJunctionTransistor(celem.id.ToString(), celem.pol2.name, celem.pol1.name, celem.pol3.name, "0", bjtModelPNP.Name));
                        
                    }


                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "ib"));
                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "ic"));
                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "vbe"));
                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "vbc"));



                }
                else if (celem.type == 0)
                {
                    ckt.Add(new VoltageSource(celem.id.ToString(), celem.pol1.name, celem.pol2.name, 0));
                    Debug.Log("celem.id.ToString() " + celem.id.ToString());
                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "i"));
                    //currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "v"));

                }
                else if (celem.type == -1)
                {
                    ckt.Add(new VoltageSource(celem.id.ToString(), celem.pol1.name, celem.pol2.name, 0));

                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "i"));
                    //currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "v"));

                }
                else if (celem.type == -2)
                {
                    ckt.Add(new VoltageSource(celem.id.ToString(), celem.pol1.name, celem.pol2.name, 0));

                    currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "i"));
                    //currentExports.Add(new RealPropertyExport(sim, celem.id.ToString(), "v"));

                }
            }
        }
        catch (Exception)
        {
            startstop.setIsStartedFalse();
        }
        
        Debug.Log("start");
        Debug.Log("");
        // New Thread for continues analysing without blocking the whole app
        Task t1 = Task.Run(() => analyse(ckt, sim));
        
       

    }

    // Analysing Method, executed from the new thread
    public void analyse(Circuit ckt, Transient sim)
    {

        stopAnalysis = false;
        DateTime startTime = DateTime.Now;

        List<Double> currentExportsValues = new List<Double>();


        Debug.Log("ckt Count: " + ckt.Count);
        Debug.Log("currentExports Count: " + currentExports.Count);

        //var currentExport = new RealPropertyExport(sim, "LED", "id");
        Debug.Log("2");

        // Method to access the export data while analysing
        sim.ExportSimulationData += (sender, args) =>
        {
            Debug.Log("export start");
            while (DateTime.Now.Ticks < startTime.AddSeconds(args.Time * (1d / analyseSpeed)).Ticks && !stopAnalysis)
            {
                //sleep
                //Debug.Log("wait" );
            }

          



            foreach (var item in currentExports)
            {
                currentExportsValues.Add(item.Value);
                Debug.Log("item.value: " + item.Value);
                
            }



            Debug.Log("export celem count: " + cElements.Count);
            // Export the Values depending of type
            foreach (CircuitElement celem in cElements)
            {

                
                if(celem.type == 4)
                {
                    celem.ib = currentExportsValues[0];
                    currentExportsValues.RemoveAt(0);
                    celem.ic = currentExportsValues[0];
                    currentExportsValues.RemoveAt(0);
                    celem.vbe = currentExportsValues[0];
                    currentExportsValues.RemoveAt(0);
                    celem.vbc = currentExportsValues[0];
                    currentExportsValues.RemoveAt(0);
                }
                else if (celem.type > 0)
                {
                    Debug.Log("current: " + currentExports[0]);
                    celem.current = currentExportsValues[0];
                    currentExportsValues.RemoveAt(0);
                    celem.voltage = currentExportsValues[0];
                    currentExportsValues.RemoveAt(0);



                }
                else if (celem.type <= 0)
                {
                    celem.current = currentExportsValues[0];
                    currentExportsValues.RemoveAt(0);
                }

            }

            if (stopAnalysis )
            {
                Debug.Log(stopAnalysis + "bool");
                
                startstop.setIsStartedFalse();
                currentExports.Clear();
                // Stop sim 
                sim.TimeParameters.StopTime = 0.0;
            }
        };
        Debug.Log("start");
        sim.Run(ckt);
        Debug.Log("finish");
        currentExports.Clear();
        startstop.setIsStartedFalse();
    }
}








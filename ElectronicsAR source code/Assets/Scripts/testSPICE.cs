using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using System.Threading.Tasks;



// Test class used in exampleScene to test the SpiceSharp behavior
public class testSPICE : MonoBehaviour
{
    // Start is called before the first frame update

    public Task t1;
    public Boolean next = false;
    
   
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        t1 = new Task(test);
        t1.Start();
        //test();


    }

    public void test()
    {
        //DateTime simTime = DateTime.Now;
        DateTime startTime = DateTime.Now;

        //        await Task.Delay(1000);

        var dModel = new DiodeModel("test");
        var bjtModelNPN = new BipolarJunctionTransistorModel("bjtModel");
        bjtModelNPN.SetParameter("npn", true)
                   .SetParameter("bf", 200.0)
                   .SetParameter("cjc", 20e-12)
                   .SetParameter("cje", 20e-12)
                   .SetParameter("is", 1e-16);








        var ckt0 = new Circuit(
            new VoltageSource("V1", "a", "0", 5),
            new Diode("LED1", "b", "bb", dModel.Name),
            new Diode("LED2", "e", "ee", dModel.Name),
            new Resistor("R1", "a", "b", 330),
            new Resistor("R2", "a", "c", 1000),
            new Resistor("R3", "a", "d", 1000),
            new Resistor("R4", "a", "e", 320),
            new Capacitor("C1", "b", "c", 18e-6),
            new Capacitor("C2", "e", "d", 18e-6),
            new BipolarJunctionTransistor("T1", "bb", "d", "0", "0", bjtModelNPN.Name),
            new BipolarJunctionTransistor("T2", "ee", "c", "0", "0", bjtModelNPN.Name),
            bjtModelNPN,
            dModel
            );


        var ckt = new Circuit(
            new VoltageSource("V1", "a", "0", 12),
            new Diode("LED1", "a", "aa", dModel.Name),
            new Diode("LED2", "a", "ab", dModel.Name),
            new Resistor("R1", "aa", "b", 1200),
            new Resistor("R2", "a", "c", 30000),
            new Resistor("R3", "a", "d", 30000),
            new Resistor("R4", "ab", "e", 1200),
            new Capacitor("C1", "b", "c", 10e-9),
            new Capacitor("C2", "e", "d", 10e-9),
            new BipolarJunctionTransistor("T1", "b", "d", "0", "0", bjtModelNPN.Name),
            new BipolarJunctionTransistor("T2", "e", "c", "0", "0", bjtModelNPN.Name),
            bjtModelNPN,
            dModel
            );

        var ckt2 = new Circuit(
            new VoltageSource("V1", "a", "0", 9),
            // new VoltageSource("V2", "d", "0", 3),
            new Resistor("R1", "a", "0", 1000)
            
            );


        var ckt_ref = new Circuit(
                new VoltageSource("V1", "in", "0", 9),
                new VoltageSource("Vsupply", "vdd", "0", 5.0),
                new Resistor("R1", "vdd", "out", 10.0e3),
                new Resistor("R2", "in", "b", 1.0e3),
                new BipolarJunctionTransistor("Q1", "out", "b", "0", "0", bjtModelNPN.Name),
                new BipolarJunctionTransistor("Q2", "out", "b", "0", "0", bjtModelNPN.Name),
                bjtModelNPN);

        //var trans = new DC("DC", "V1", 9.0, 9.0, 0.1);
        var trans = new Transient("trans", 0.00000001, 4);

        /* trans.TimeParameters.InitialConditions["b"] = 12;
         trans.TimeParameters.InitialConditions["c"] = 1;
         trans.TimeParameters.InitialConditions["d"] = -9;
         trans.TimeParameters.InitialConditions["e"] = 0;

         */

        trans.TimeParameters.InitialConditions["b"] = 12; // 5
        trans.TimeParameters.InitialConditions["c"] = 1; //1
        trans.TimeParameters.InitialConditions["d"] = -1; // -1
        trans.TimeParameters.InitialConditions["e"] = 0;


        var voltageExport = new RealPropertyExport(trans, "T1", "vbc");
        var voltageExport2 = new RealPropertyExport(trans, "T1", "vbe");
        var voltageExport9 = new RealPropertyExport(trans, "T2", "vbc");
        var voltageExport10 = new RealPropertyExport(trans, "T2", "vbe");
        var ledExport1 = new RealPropertyExport(trans, "LED1", "i");
        var ledExport2 = new RealPropertyExport(trans, "LED2", "i");
        /*var voltageExport = new RealPropertyExport(trans, "R1", "v");
        var voltageExport2 = new RealPropertyExport(trans, "R2", "v");
        var voltageExport3 = new RealPropertyExport(trans, "R3", "v");
        var voltageExport4 = new RealPropertyExport(trans, "R4", "v");*/
        //var voltageExport5 = new RealPropertyExport(trans, "C1", "v");
        //var voltageExport6 = new RealPropertyExport(trans, "C2", "v");
        /* var voltageExport7 = new RealPropertyExport(trans, "T1", "vbe");
         var voltageExport8 = new RealPropertyExport(trans, "T1", "vce");
         var voltageExport9 = new RealPropertyExport(trans, "T2", "vbe");
         var voltageExport10 = new RealPropertyExport(trans, "T2", "vce");

         var CurrentExport = new RealPropertyExport(trans, "R1", "i");
         var CurrentExport2 = new RealPropertyExport(trans, "R2", "i");
         var CurrentExport3 = new RealPropertyExport(trans, "R3", "i");
         var CurrentExport4 = new RealPropertyExport(trans, "R4", "i");
         var CurrentExport5 = new RealPropertyExport(trans, "C1", "i");
         var CurrentExport6 = new RealPropertyExport(trans, "C2", "i");*/
        // var CurrentExport7 = new RealPropertyExport(trans, "T1", "ic");
        //var CurrentExport8 = new RealPropertyExport(trans, "T1", "ib");
        //var CurrentExport9 = new RealPropertyExport(trans, "T2", "ic");
        //var CurrentExport10 = new RealPropertyExport(trans, "T2", "ib");
        //var t = new RealCurrentExport(trans, "R1");
        //var t = new RealPropertyExport(trans, "R1", "i");
        //currentExports.Add(new RealPropertyExport(trans, "R1", "i"));
        //var t1 = new RealVoltageExport(trans, "a");
        int i = 0;
        trans.ExportSimulationData +=  (sender, args) =>
        {
            
            //Debug.Log(args.GetVoltage("a"));
            i++;
            //Debug.Log(i + "          " + args.Time);
            //Debug.Log("b: " + args.GetVoltage("b"));
            //Debug.Log("c: " + args.GetVoltage("c"));
            //Debug.Log("d: " + args.GetVoltage("d"));
            //Debug.Log("e: " + args.GetVoltage("e"));
            Debug.Log("vbc" + voltageExport.Value);
            Debug.Log("vbe" + voltageExport2.Value);
            Debug.Log("vbc2 " + voltageExport9.Value);
            Debug.Log("vbe2 " + voltageExport10.Value);
            Debug.Log("LED1 " + ledExport1.Value);
            Debug.Log("LED2 " + ledExport2.Value);
            //Debug.Log("C1 " + voltageExport5.Value);
            //Debug.Log("C2 " + voltageExport6.Value);
            ///*var voltage = voltageExport.Value;
            //var voltage2 = voltageExport2.Value;
            //var voltage3 = voltageExport3.Value;
            //var voltage4 = voltageExport4.Value;
            //var voltage5 = voltageExport5.Value;
            //var voltage6 = voltageExport6.Value;
            //var voltage7 = voltageExport7.Value;
            //var voltage8 = voltageExport8.Value;
            //var voltage9 = voltageExport9.Value;
            //var voltage10 = voltageExport10.Value;
            //var current = CurrentExport.Value;
            //var current2 = CurrentExport2.Value;
            //var current3 = CurrentExport3.Value;
            //var current4 = CurrentExport4.Value;
            //var current5 = CurrentExport5.Value;
            //var current6 = CurrentExport6.Value;*/
            //var current7 = CurrentExport7.Value;
            //var current8 = CurrentExport8.Value;
            // Debug.Log("t1 ic:" + current7);
            //Debug.Log("t1 ib:" + current8);
            //Debug.Log("t2 ic:" + CurrentExport9.Value);
            //Debug.Log("t2 ib:" + CurrentExport10.Value);

            //Debug.Log(t.Value);
            //await Task.Delay(1000);
            //t1.Wait(Task.Delay(1000));
            //threa

            //Task tas = new Task(async () => {
            //    await Task.Delay(1000);
            //});
            //task.Start();
            //task.Wait();
            //task.Result.Wait();
            //await new WaitForSeconds(10);

            //Task t2 = new Task(wait);
            //t2.Start();
            //t2.Wait();
            
            //simTime = simTime.AddSeconds(args.Time);
            //Debug.Log("NOW " + DateTime.Now.Ticks);
            //Debug.Log("SimTime " + startTime.AddSeconds(args.Time).Ticks);
            while (DateTime.Now.Ticks < startTime.AddSeconds(args.Time).Ticks)
            {
                if (next)
                {
                    return;
                    // DateTime.Now.Ticks
                    //startTime.AddSeconds(args.Time).Ticks
                }
                //Debug.Log(DateTime.Now.CompareTo(simTime));
                Debug.Log("wait");
                //Debug.Log(Time.realtimeSinceStartupAsDouble);
                // await Task.Yield();
            }
           // Debug.Log("after wait");

        };
        Debug.Log("start");
        trans.Run(ckt);
        //Task t2 = Task.Run(() => analyse(ckt2, trans));
        Debug.Log("finish");
    }

    // Update is called once per frame
    void Update()
    {
        //this.GetComponent<Transform>().position.Set(GetComponent<Transform>().position.x + 1 , 1, 1);
        //Debug.Log("update");
    }

    async void wait()
    {
        await Task.Delay(1000);
    }
    List<RealPropertyExport> currentExports = new List<RealPropertyExport>();
    public void analyse(Circuit ckt, Transient sim)
    {

        DateTime startTime = DateTime.Now;

        List<Double> currentExportsValues = new List<Double>();


        Debug.Log("1");
        //var currentExport = new RealPropertyExport(sim, "LED", "id");
        Debug.Log("2");

        sim.ExportSimulationData += (sender, args) =>
        {
            Debug.Log("export start");
            while (DateTime.Now.Ticks < startTime.AddSeconds(args.Time).Ticks * (1 / 1))
            {
                //sleep
                Debug.Log("wait");
            }

            foreach (var item in currentExports)
            {
                currentExportsValues.Add(item.Value);

            }


            Debug.Log(currentExportsValues[0]);
            currentExportsValues.RemoveAt(0);




            Debug.Log("export celem count: " );

        };
        Debug.Log("start");
        sim.Run(ckt);

    }



}

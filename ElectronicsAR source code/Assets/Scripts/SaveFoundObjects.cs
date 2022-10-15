using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveFoundObjects : MonoBehaviour
{
    List<GameObject> objects = new List<GameObject>();
    public elemtPositions elemtPositions;
    public GameObject parent;
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
       // elemtPositions.reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Register an object if it was recogonized
    public void registerObject(GameObject obj)
    {
        objects.Add(obj);
    }

    // Deregister an Object if it is not langer detected
    public void deregisterObject(GameObject obj)
    {
        objects.Remove(obj);
    }


    [Obsolete]
    public void saveToSOchangeToAnalyser()
    {
        //elemtPositions.reset();
        Debug.Log("objectsCount: " + objects.Count);
        foreach (GameObject obj in objects)
        {
         /*   CircuitElement elem = obj.GetComponent<CircuitElement>();
            Quaternion q = Quaternion.Inverse(parent.transform.rotation) * obj.transform.rotation;
            Vector3 position = parent.transform.InverseTransformPoint(obj.transform.position);
            elemtPositions.add(obj, position, q, elem);
         */
            
        }

        //elemtPositions.sortList(elemtPositions.listOfFoundElements);
        SceneManager.LoadScene("Analyser");
    }

}

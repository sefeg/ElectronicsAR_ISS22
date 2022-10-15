using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject to manage sElementSO and positions of detected elements
[CreateAssetMenu(fileName = "Element Positions")]
public class elemtPositions : ScriptableObject
{
      public List<(Vector3, Quaternion, cElementSO)> listOfFoundElements = new List<(Vector3 position, Quaternion rot, cElementSO celem)>{};
    public Hashtable foundElements = new Hashtable();
      public List< cElementSO> listOfFoundElementsAll = new List<cElementSO>{};
    public List<int> savedIds = new List<int>();
    Comp comparer = new Comp();

    // Add a new element to the SO
    public void add(GameObject obj, Vector3 position, Quaternion rot, CircuitElement elem)
    {
        //int index = listOfFoundElements.BinarySearch((position, rot, elem), comparer);
        
        int index = savedIds.BinarySearch(elem.id);



        Debug.Log("add " + savedIds.Count + " index " + index);
        
        if (index<0)
        {
            cElementSO tmp = ScriptableObject.CreateInstance("cElementSO") as cElementSO;
            savedIds.Add(elem.id);
            copyCircuitElement(elem, tmp);
            listOfFoundElements.Add((position, rot, tmp));
            listOfFoundElementsAll.Add(tmp);

            foundElements.Add(elem.id, (position, rot, tmp));

            Debug.Log("new scriptableObject");
            if (elem.type == -2)
            {
                tmp = ScriptableObject.CreateInstance("cElementSO") as cElementSO;
                savedIds.Add(elem.id+1);
                // add -40 dgree
                tmp.type = -1;
                tmp.id = elem.id + 1;
                rot *= Quaternion.Euler(0, 0, -40);
                listOfFoundElements.Add((position, rot, tmp));
                listOfFoundElementsAll.Add(tmp);

                foundElements.Add(tmp.id, (position, rot, tmp));

            }
            savedIds.Sort();
            listOfFoundElementsAll.Sort(comparer);
        }
        else
        {

            pasteCircuitElement( listOfFoundElementsAll[index], elem);
            listOfFoundElements.Add((position, rot, listOfFoundElementsAll[index]));
            foundElements.Add(elem.id, (position, rot, listOfFoundElementsAll[index]));
            if (elem.type == -2)
            {

                listOfFoundElements.Add((position, rot, listOfFoundElementsAll[index+1]));

                foundElements.Add(elem.id+1, (position, rot, listOfFoundElementsAll[index + 1]));
            }
        }
        
        
        
    }


    public void removeFromList(int id)
    {
        foundElements.Remove(id);

    }


    // Write Back the data of all elements in clist
    public void writeBack(List<CircuitElement> clist)
    {
        listOfFoundElements.Clear();
        foundElements.Clear();
        Debug.Log("listofFoundCount" + listOfFoundElements.Count);
        foreach (CircuitElement celem in clist)
        {
            Debug.Log("celem.id" + celem.id);
            int index = savedIds.BinarySearch(celem.id);
            copyCircuitElement(celem, listOfFoundElementsAll[index]);

        }
        
    }

    //from obj to SO
    public void copyCircuitElement(CircuitElement source, cElementSO destination)
    {
        destination.id = source.id;
        destination.elementName = source.elementName;
        destination.value = source.value;
        destination.type = source.type;  
        destination.npn = source.npn;
        destination.precondition = source.precondition;
        Debug.Log(source.elementName);
    }
    //from SO to Obj
    public void pasteCircuitElement(cElementSO source, CircuitElement destination)
    {
        if (source ==null)
        {
            Debug.Log("source = null");
        }
        destination.id = source.id;
        destination.elementName = source.elementName;
        destination.value = source.value;
        destination.type = source.type;
        destination.npn = source.npn;
        destination.precondition = source.precondition;
        Debug.Log(source.elementName);
    }


}


// Comparer for cElementSO to sort the list
internal class Comp : IComparer<cElementSO>
{
    public int Compare(cElementSO c1, cElementSO c2)
    {
        
        if (c1 == null)
        {
            if (c2 == null)
            {
                
                return 0;
            }
            else
            {
                Debug.Log("c1 == null");
                return -1;
            }

        }
        else
        {
            if (c2 == null)
            {
                Debug.Log("c2 == null");
                return 1;
            }
            else
            {
                Debug.Log("c1 compareto c2 " + c1.id.CompareTo(c2.id));
                return c1.id.CompareTo(c2.id);
            }
        }
    }
}






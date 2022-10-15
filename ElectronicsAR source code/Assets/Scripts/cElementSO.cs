using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject th store the data between the scences
public class cElementSO : ScriptableObject
{
    public int id;
    public string elementName;
    public int type;
    public double value;

    public int npn;
    public int precondition;
}

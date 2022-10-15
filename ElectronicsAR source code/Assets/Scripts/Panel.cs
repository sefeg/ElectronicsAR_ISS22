using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class to used a generic panel dependent of the circuit element type
public class Panel : MonoBehaviour
{
    public int CircuitElementType = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(int type)
    {
        CircuitElementType = type;
    }
}

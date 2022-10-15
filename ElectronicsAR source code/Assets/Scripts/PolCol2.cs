using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Obsolete]
public class PolCol2 : MonoBehaviour
{
    public GameObject CircuitElemt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Obsolete("new generic Method ist in PolCol")]
    private void OnTriggerEnter(Collider other)
    {
        CircuitElemt.GetComponent<CircuitElement>().pol2 =other.gameObject;
        Debug.Log("triggerEnter");
    }
}

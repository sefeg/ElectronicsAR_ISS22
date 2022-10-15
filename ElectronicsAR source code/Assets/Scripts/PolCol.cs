using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to detect collision with the boards nodes
public class PolCol : MonoBehaviour
{

    public GameObject CircuitElemt;
    public int pol = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called if collision with board.
    // wirte the corresponding node to the CircuitElement
    private void OnTriggerEnter(Collider other)
    {
        if (pol == 1)
        {
            CircuitElemt.GetComponent<CircuitElement>().pol1 = other.gameObject;
            this.gameObject.SetActive(false);
            
        }
        else if (pol ==2)
        {
            CircuitElemt.GetComponent<CircuitElement>().pol2 = other.gameObject;
            this.gameObject.SetActive(false);
        }
        else if (pol ==3)
        {
            CircuitElemt.GetComponent<CircuitElement>().pol3 = other.gameObject;
            this.gameObject.SetActive(false);
        }
        
        Debug.Log("triggerEnter");
    }
}

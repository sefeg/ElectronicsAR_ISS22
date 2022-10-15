using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Information panels align always to camera
public class panelToCam : MonoBehaviour
{
    public Transform lookAtObject;


    void Start()
    {
        lookAtObject = Camera.main.transform;
    }
    void Update()
    {
        if (lookAtObject)
        {
            Vector3 directionToTarget = this.lookAtObject.position - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(-directionToTarget, this.lookAtObject.up);
        }
    }
}

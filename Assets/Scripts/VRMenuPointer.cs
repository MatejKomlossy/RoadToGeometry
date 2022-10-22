using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMenuPointer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {      
           transform.position = hit.point;
        }
    }
}

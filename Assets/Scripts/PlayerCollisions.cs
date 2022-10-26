using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void CubeCollected()
    {
        
    }

    private void SphereCollected()
    {
        
    }

    private void CylinderCollected()
    {
        
    }
    
    private void CapsuleCollected()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            CubeCollected();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Sphere"))
        {
            SphereCollected();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Cylinder"))
        {
            CylinderCollected();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Capsule"))
        {
            CapsuleCollected();
            Destroy(other.gameObject);
        }
    }
}

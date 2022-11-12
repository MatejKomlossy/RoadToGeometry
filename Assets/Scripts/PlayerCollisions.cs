using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    public AudioSource obstacleCollisionSound;
    private const string CubeTag = "Cube";
    private const string SphereTag = "Sphere";
    private const string CylinderTag = "Cylinder";
    private const string CapsuleTag = "Capsule";
    private readonly string[] _collectibleTags = {CubeTag, SphereTag, CylinderTag, CapsuleTag};
    
    private void OnTriggerEnter(Collider other)
    {
        foreach (var tagStr in _collectibleTags)
        {
            if (other.CompareTag(tagStr))
            {
                EventManager.Instance.ObjectCollected(other.gameObject);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.GetComponent<GameOver>().EndGame();
        obstacleCollisionSound.Play();
    }
}

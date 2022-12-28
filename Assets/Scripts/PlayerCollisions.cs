using System;
using System.Collections;
using System.Collections.Generic;
using Tasks;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    public AudioSource obstacleCollisionSound;
    public AudioSource correctObjectCollisionSound;
    public AudioSource wrongObjectCollisionSound;

    public GameObject taskManagerObject;

    private const string CubeTag = "Cube";
    private const string CuboidTag= "Cuboid";
    private const string SphereTag = "Sphere";
    private const string CylinderTag = "Cylinder";
    private const string CapsuleTag = "Capsule";
    private readonly string[] _collectibleTags = {CubeTag, CuboidTag, SphereTag, CylinderTag, CapsuleTag};
    
    private void OnTriggerEnter(Collider other)
    {
        foreach (var tagStr in _collectibleTags)
        {
            if (other.CompareTag(tagStr))
            {
                TaskManager taskManager = taskManagerObject.GetComponent<TaskManager>();
                bool isObjectFromTheTask = taskManager.CurrentTask.IsObjectFromTheTask(other.gameObject);
                if (isObjectFromTheTask) correctObjectCollisionSound.Play();
                else wrongObjectCollisionSound.Play();

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

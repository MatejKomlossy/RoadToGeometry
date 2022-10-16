using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float forwardMovementSpeed = 10f;
    public SpawnManager spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * (Time.deltaTime * forwardMovementSpeed); //forward, comrades

        Quaternion headRotation = Camera.main.transform.rotation;
        Vector3 currentEulerAngles = headRotation.eulerAngles;

        float sideMovementSpeed = forwardMovementSpeed / 5;

        if (currentEulerAngles.z > 190 && currentEulerAngles.z < 350)
        {
            if (transform.position.x > -20) //NEFUNGUJE ta hranica
            {
                transform.position -= Vector3.left * (Time.deltaTime * sideMovementSpeed); //to the right, comrades
            }
        }
        else if (currentEulerAngles.z > 10 && currentEulerAngles.z < 70)
        {
            if (transform.position.x < 20) //NEFUNGUJE ta hranica
            {
                transform.position += Vector3.left * (Time.deltaTime * sideMovementSpeed); //to the left, comrades
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }
}

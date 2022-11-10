using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardMovementSpeed = 10f;
    public SpawnManager spawnManager;

    public float playerMinX = 7.6f;
    public float playerMaxX = 11.3f;

    private const float MinAnglesLeft = 10;
    private const float MaxAnglesLeft = 70;
    private const float MinAnglesRight = 190;
    private const float MaxAnglesRight = 350;

    private float decelerationSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {

        if (transform.GetComponent<GameOver>().isGameOver) //slow down after Game Over
        {
            if (forwardMovementSpeed > 0.0f)
            {
                transform.position += Vector3.forward * (Time.deltaTime * forwardMovementSpeed);
                forwardMovementSpeed -= decelerationSpeed;
            }
        }

        transform.position += Vector3.forward * (Time.deltaTime * forwardMovementSpeed); //forward, comrades
        
        Quaternion headRotation = Camera.main.transform.rotation;
        Vector3 currentEulerAngles = headRotation.eulerAngles;
        
        float sideMovementSpeed = forwardMovementSpeed / 5;
        
        if (currentEulerAngles.z > MinAnglesRight && currentEulerAngles.z < MaxAnglesRight)
        {
            if (transform.position.x < playerMaxX)
            {
                transform.position -= Vector3.left * (Time.deltaTime * sideMovementSpeed); //to the right, comrades
            }
        }
        else if (currentEulerAngles.z > MinAnglesLeft && currentEulerAngles.z < MaxAnglesLeft)
        {
            if (transform.position.x > playerMinX)
            {
                transform.position += Vector3.left * (Time.deltaTime * sideMovementSpeed); //to the left, comrades
            }
        }
        
        
        // PC
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * (Time.deltaTime * forwardMovementSpeed);   
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(transform.position.x > playerMinX)
                transform.position += Vector3.left * (Time.deltaTime * forwardMovementSpeed);    
        }
        if (Input.GetKey(KeyCode.D))
        {
            if(transform.position.x < playerMaxX)
                transform.position += Vector3.right * (Time.deltaTime * forwardMovementSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnTriggerEntered();    
        }
    }
}
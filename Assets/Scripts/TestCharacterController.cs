using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * (Time.deltaTime * movementSpeed);    
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * (Time.deltaTime * movementSpeed);    
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * (Time.deltaTime * movementSpeed);    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }
}

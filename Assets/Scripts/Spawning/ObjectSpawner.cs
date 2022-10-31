using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour      //attached on a road GO. Will be called from RoadSpawner
{
    public GameObject obstacle;     //can be a List later on
    public List<GameObject> collectibles;           //4
    public List<GameObject> positionHolders;        //10
    public Transform obstacleParent;
    public Transform collectibleParent;

    private const int NumOfCollectibles = 2;
    private const int NumOfObstacles = 2;
    private const int ObstacleSpawnHeight = 5;
    private const float CollectibleSpawnHeight = 0.5f;
    
    static System.Random _random = new System.Random();

    private bool FloatEquals(float value1, float value2)
    {
        return Math.Abs(value1 - value2) <= 0.01f;
    }

    public void SpawnObjects()
    {
        var availablePosHolders = SpawnObstacles(new List<GameObject>(positionHolders));
        SpawnCollectibles(availablePosHolders);
    }
    
    private GameObject SpawnObject(List<GameObject> availablePosHolders, GameObject objectToSpawn, Transform parent, Vector3 shift)
    {
        var index = _random.Next(availablePosHolders.Count);
        var usedPosHolder = availablePosHolders[index];
        var objectInstance = Instantiate(objectToSpawn, parent);
        objectInstance.transform.position = usedPosHolder.transform.position + shift;
        return usedPosHolder;
    }

    private List<GameObject> SpawnObstacles(List<GameObject> availablePosHolders)
    {
        for (int i = 0; i < NumOfObstacles; i++)
        {
            var usedPosHolder = SpawnObstacle(availablePosHolders);
            availablePosHolders = PosHoldersAfterObstacle(availablePosHolders, usedPosHolder);
        }

        return availablePosHolders;
    }

    private GameObject SpawnObstacle(List<GameObject> availablePosHolders)  //returns posHolder
    {
        var shift = Vector3.up * ObstacleSpawnHeight;
        return SpawnObject(availablePosHolders, obstacle, obstacleParent, shift);
    }
    
    private List<GameObject> PosHoldersAfterObstacle(List<GameObject> availablePosHolders, GameObject usedPosHolder)
    {
        return availablePosHolders
            .Where(ph => !FloatEquals(ph.transform.position.z,usedPosHolder.transform.position.z))
            .ToList();
    }

    private void SpawnCollectibles(List<GameObject> availablePosHolders)
    {
        for (int i = 0; i < NumOfCollectibles; i++)
        {
            var usedPosHolder = SpawnCollectible(availablePosHolders);
            availablePosHolders = PosHoldersAfterCollectible(availablePosHolders, usedPosHolder);
        }
    }
    
    //note: collectibles should have trigger colliders and shouldn't use gravity
    private GameObject SpawnCollectible(List<GameObject> availablePosHolders)   //returns posHolder
    {
        var index = _random.Next(collectibles.Count);
        var shift = Vector3.up * CollectibleSpawnHeight;
        return SpawnObject(availablePosHolders, collectibles[index], collectibleParent, shift);
    }   
    
    private List<GameObject> PosHoldersAfterCollectible(List<GameObject> availablePosHolders, GameObject usedPosHolder)
    {
        return availablePosHolders.Where(ph => ph != usedPosHolder).ToList();
    }

    //Clears obstacles and collectibles. Called when moving road
    public void ClearObjects()
    {
        foreach (Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in collectibleParent)
        {
            Destroy(child.gameObject);
        }
    }
}

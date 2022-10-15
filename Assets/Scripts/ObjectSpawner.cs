using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour      //attached on a road GO. Will be called from RoadSpawner
{
    public GameObject obstacle;     //can be a List later on
    public List<GameObject> positionHolders;        //10
    public Transform obstacleParent;
    public Transform collectibleParent;

    private const int NumOfCollectibles = 2;
    private const int NumOfObstacles = 2;
    
    static System.Random _random = new System.Random();

    private bool FloatEquals(float value1, float value2)
    {
        var difference = Math.Abs(value1 * 0.01f);
        return Math.Abs(value1 - value2) <= difference;
    }

    public void SpawnObjects()
    {
        var availablePosHolders = SpawnObstacles();
        SpawnCollectibles(availablePosHolders);
    }
    
    private GameObject SpawnObject(List<GameObject> availablePosHolders, GameObject objectToSpawn, Transform parent)
    {
        var index = _random.Next(availablePosHolders.Count);
        var posHolder = availablePosHolders[index];
        Instantiate(objectToSpawn, parent);
        return posHolder;
    }

    private List<GameObject> SpawnObstacles()
    {
        var availablePosHolders = new List<GameObject>(positionHolders);
        for (int i = 0; i < NumOfObstacles; i++)
        {
            var usedPosHolder = SpawnObstacle(availablePosHolders);
            availablePosHolders = PosHoldersAfterObstacle(availablePosHolders, usedPosHolder);
        }

        return availablePosHolders;
    }

    private GameObject SpawnObstacle(List<GameObject> availablePosHolders)  //returns posHolder
    {
        return SpawnObject(availablePosHolders, obstacle, obstacleParent);
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
    
    private GameObject SpawnCollectible(List<GameObject> availablePosHolders)   //returns posHolder
    {
        return null; //implement similar to SpawnObstacle
    }
    
    private List<GameObject> PosHoldersAfterCollectible(List<GameObject> availablePosHolders, GameObject usedPosHolder)
    {
        return new List<GameObject>(); //implement
    }

    public void ClearObjects()
    {
        //implement
        //clear obstacles and collectibles
        //call this when moving road
    }
}

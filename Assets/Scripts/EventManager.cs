using System;
using JetBrains.Annotations;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    [CanBeNull] public event Action<int> TaskCompletedEvent; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void TaskCompleted(int points)
    {
        TaskCompletedEvent?.Invoke(points);
    }
}
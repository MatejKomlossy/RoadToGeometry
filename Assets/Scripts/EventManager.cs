using System;
using JetBrains.Annotations;
using Tasks;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    [CanBeNull] public event Action<Task> TaskCompletedEvent; 

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

    public void TaskCompleted(Task task)
    {
        TaskCompletedEvent?.Invoke(task);
    }
}
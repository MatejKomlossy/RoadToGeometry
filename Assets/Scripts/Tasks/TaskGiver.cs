using System;
using UnityEngine;

namespace Tasks
{
    public class TaskGiver : MonoBehaviour
    {
        private void Start()
        {
            EventManager.Instance.TaskCompletedEvent += DisplayTask;
        }

        private void DisplayTask(Task task)
        {
            //implement
        }
    }
}
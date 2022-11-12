using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tasks
{
    public class TaskManager : MonoBehaviour
    {
        public TextMeshProUGUI taskText, scoreText;
        public List<GameObject> objectPrefabs;
        public List<string> cubeHints;
        public List<string> sphereHints;
        public List<string> cylinderHints;
        public List<string> capsuleHints;
        private Dictionary<string, List<string>> _collectiblesHints = new ();
        private static System.Random _random = new System.Random();
        private Task CurrentTask { get; set; }
        
        private const string CubeTag = "Cube";
        private const string SphereTag = "Sphere";
        private const string CylinderTag = "Cylinder";
        private const string CapsuleTag = "Capsule";

        private void Start()
        {
            InitCollectiblesHints();
            NewCurrentTask();
            EventManager.Instance.TaskCompletedEvent += OnTaskCompleted;
            EventManager.Instance.ObjectCollectedEvent += OnObjectCollected;
        }

        private void InitCollectiblesHints()
        {
            _collectiblesHints.Add(CubeTag, cubeHints);
            _collectiblesHints.Add(SphereTag, sphereHints);
            _collectiblesHints.Add(CylinderTag, cylinderHints);
            _collectiblesHints.Add(CapsuleTag, capsuleHints);
        }

        private void DisplayTask(Task task)
        {
            var textToDisplay = string.Join("\n", task.TaskStrings());

        }

        private Task CreateNewTask()
        {
            return new Task(objectPrefabs, Hints());
        }

        private void NewCurrentTask()
        {
            var task = CreateNewTask();
            CurrentTask = task;
            DisplayTask(task);
        }

        private void OnTaskCompleted(Task task)
        {
            AddPoints(task.Points);
            NewCurrentTask();
        }

        private void AddPoints(int points)
        {
            //implement
        }

        private void OnObjectCollected(GameObject go)
        {
            CurrentTask.Collect(go);
            DisplayTask(CurrentTask); //number of objects left to collect changes
        }

        private Dictionary<string, string> Hints()
        {
            Dictionary<string, string> tagsHints = new();
            foreach (var collectibleHint in _collectiblesHints)
            {
                var tagStr = collectibleHint.Key;
                var possibleHints = collectibleHint.Value;
                var randomHint = possibleHints[_random.Next(possibleHints.Count)];
                tagsHints.Add(tagStr, randomHint);
            }

            return tagsHints;
        }
    }
}
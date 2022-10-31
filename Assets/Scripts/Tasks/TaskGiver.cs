using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    public class TaskGiver : MonoBehaviour
    {
        public List<GameObject> objectPrefabs;
        public List<string> cubeHints;
        public List<string> sphereHints;
        public List<string> cylinderHints;
        public List<string> capsuleHints;
        private Dictionary<string, List<string>> _collectiblesHints = new ();
        private static System.Random _random = new System.Random();

        private void Start()
        {
            InitCollectiblesHints();
            DisplayTask(new Task(objectPrefabs, Hints()));
            EventManager.Instance.TaskCompletedEvent += OnTaskCompleted;
        }

        private void InitCollectiblesHints()
        {
            _collectiblesHints.Add("Cube", cubeHints);
            _collectiblesHints.Add("Sphere", sphereHints);
            _collectiblesHints.Add("Cylinder", cylinderHints);
            _collectiblesHints.Add("Capsule", capsuleHints);
        }

        private void DisplayTask(Task task)     
        {
            //implement
            Debug.Log(string.Join("\n", task.TaskStrings()));
        }

        private void OnTaskCompleted(Task task)
        {
            AddPoints(task.Points);
            DisplayTask(new Task(objectPrefabs, Hints()));
        }

        private void AddPoints(int points)
        {
            //implement
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
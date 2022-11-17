using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks
{
    public class Task
    {
        private readonly List<GameObject> _objectPrefabs;
        
        private Dictionary<string, int> _objectsToCollect = new();  //<tag, count>
        private Dictionary<string, string> _tagsHints;  //<tag, hint>
        private const int MaxObjectKinds = 3;
        private const int MaxOneObjectCount = 3;
        private static System.Random _random;
        private const float ChanceToAddObject = 0.5f;
        private const int PointsPerObject = 10;
        public int Points { get; private set; }

        public AudioSource correctObjectCollisionSound = GameObject.Find("CorrectObjectCollision").GetComponent<AudioSource>();
        public AudioSource wrongObjectCollisionSound = GameObject.Find("WrongObjectCollision").GetComponent<AudioSource>();

        public Task(List<GameObject> objectPrefabs, Dictionary<string, string> tagsHints)
        {
            _random = new System.Random();
            _objectPrefabs = objectPrefabs;
            _tagsHints = tagsHints;
            FillObjectsToCollect();
            ComputePoints();
        }

        private void FillObjectsToCollect()
        {
            var index = _random.Next(_objectPrefabs.Count);
            var firstObject = _objectPrefabs[index];
            _objectsToCollect.Add(firstObject.tag, RandomObjectCount());
            var objectKinds = 1;
            
            var rest = _objectPrefabs.Where(o => !o.CompareTag(firstObject.tag)).ToList();
            foreach (var objectPrefab in rest)
            {
                if (objectKinds >= MaxObjectKinds)
                {
                    return;
                }
                if (_random.NextDouble() <= ChanceToAddObject)
                {
                    _objectsToCollect.Add(objectPrefab.tag, RandomObjectCount());
                    objectKinds++;
                }
            }
        }

        private int RandomObjectCount()
        {
            return  _random.Next(MaxOneObjectCount) + 1;
        }

        public void Collect(GameObject collectible)
        {
            var tag = collectible.tag;
            if (_objectsToCollect.ContainsKey(tag) && _objectsToCollect[tag] > 0)
            {
                _objectsToCollect[tag] -= 1;
                correctObjectCollisionSound.Play();

            }
            else
            {
                wrongObjectCollisionSound.Play();
            }

            if (IsCompleted())
            {
                EventManager.Instance.TaskCompleted(this);
            }
        }

        private bool IsCompleted()
        {
            return _objectsToCollect.Count == 0 ||
                   _objectsToCollect.All(objectCount => objectCount.Value <= 0);
        }

        private void ComputePoints()
        {
            foreach (var objectCount in _objectsToCollect)
            {
                Points += objectCount.Value * PointsPerObject;
            }
        }

        public List<string> TaskStrings()   //can be displayed in rows
        {
            var result = new List<string>();
            foreach (var (tagStr, count) in _objectsToCollect)
            {
                if(count <= 0) continue;
                result.Add(count + "x " + _tagsHints[tagStr]);
            }

            return result;
        }
    }
}
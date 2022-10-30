using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks
{
    public class Task
    {
        public List<GameObject> ObjectPrefabs;
        
        private Dictionary<string, int> _objectsToCollect;  //tag, count
        private const int MaxObjectKinds = 4;
        private const int MaxOneObjectCount = 3;
        private static System.Random _random = new System.Random();
        private const float ChanceToAddObject = 0.5f;
        private const int PointsPerObject = 10;
        private int _points = 0;
        
        public Task()
        {
            FillObjectsToCollect();
            ComputePoints();
        }

        private void FillObjectsToCollect()
        {
            var index = _random.Next(ObjectPrefabs.Count);
            var firstObject = ObjectPrefabs[index];
            _objectsToCollect.Add(firstObject.tag, 0);
            var objectKinds = 1;
            
            var rest = ObjectPrefabs.Where(o => !o.CompareTag(firstObject.tag)).ToList();
            foreach (var objectPrefab in rest)
            {
                if (objectKinds > MaxObjectKinds)
                {
                    return;
                }
                if (_random.NextDouble() <= ChanceToAddObject)
                {
                    var count = _random.Next(MaxOneObjectCount) + 1;
                    _objectsToCollect.Add(objectPrefab.tag, count);
                    objectKinds++;
                }
            }
        }

        public void Collect(GameObject collectible)
        {
            var tag = collectible.tag;
            if (_objectsToCollect[tag] > 0)
            {
                _objectsToCollect[tag] -= 1;
            }

            if (IsCompleted())
            {
                EventManager.Instance.TaskCompleted(_points);
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
                _points += objectCount.Value * PointsPerObject;
            }
        }

        public List<string> TaskStrings()   //can be displayed in rows
        {
            var result = new List<string>();
            foreach (var objectCount in _objectsToCollect)
            {
                var tag = objectCount.Key;
                var gameObject = ObjectPrefabs.Find(op => op.CompareTag(tag));
                result.Add(objectCount.Value + " x " + gameObject.GetComponent<Collectible>().Name);
            }

            return result;
        }
    }
}
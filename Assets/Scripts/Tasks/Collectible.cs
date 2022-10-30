using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    public class Collectible : MonoBehaviour
    {
        public List<string> hints;
        public string Name { get; private set; }    //name is the chosen hint
        private static System.Random _random = new System.Random();
        
        private void Awake()
        {
            Name = hints[_random.Next(hints.Count)];
        }
    }
}
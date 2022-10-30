using System;
using System.Collections.Generic;
using UnityEngine;

namespace Collectibles
{
    public interface ICollectible
    {
        public List<string> Level1Hints();
        public List<string> Level2Hints();
    }
}
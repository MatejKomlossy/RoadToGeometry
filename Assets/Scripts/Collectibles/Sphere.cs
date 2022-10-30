using System.Collections.Generic;

namespace Collectibles
{
    public class Sphere : ICollectible
    {
        public List<string> Level1Hints()
        {
            return new List<string>(){"Sphere"};
        }

        public List<string> Level2Hints()
        {
            throw new System.NotImplementedException();
        }
    }
}
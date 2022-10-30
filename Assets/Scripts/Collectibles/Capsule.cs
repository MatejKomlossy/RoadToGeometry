using System.Collections.Generic;

namespace Collectibles
{
    public class Capsule : ICollectible
    {
        public List<string> Level1Hints()
        {
            return new List<string>(){"Capsule"};
        }

        public List<string> Level2Hints()
        {
            throw new System.NotImplementedException();
        }
    }
}
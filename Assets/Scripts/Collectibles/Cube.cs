using System.Collections.Generic;

namespace Collectibles
{
    public class Cube : ICollectible
    {
        public List<string> Level1Hints()
        {
            return new List<string>(){"Cube"};
        }

        public List<string> Level2Hints()
        {
            throw new System.NotImplementedException();
        }
    }
}
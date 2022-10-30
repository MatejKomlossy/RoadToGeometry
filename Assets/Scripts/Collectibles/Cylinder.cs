using System.Collections.Generic;

namespace Collectibles
{
    public class Cylinder : ICollectible
    {
        public List<string> Level1Hints()
        {
            return new List<string>(){"Cylinder"};
        }

        public List<string> Level2Hints()
        {
            throw new System.NotImplementedException();
        }
    }
}
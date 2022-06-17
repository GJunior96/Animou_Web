namespace Animou.App.Extensions
{
    public class RandomUsername
    {
        private static readonly string[] _firstCollection = { "alpha", "amazing", "bad", "black", "blue", "cringe", "dangerous", "orange", "other", "red", "white" };
        private static readonly string[] _secondCollection = { "anchor", "ambar", "bullet", "bomb", "dot", "drip", "castle", "cover", "dagger", "fire", "elm", "groove", "wizard" };
        private static readonly Random random = new Random();
        public static string CreateRandomUsername()
        {
            int firstIndex = random.Next(0, _firstCollection.Length);
            int secondIndex = random.Next(0, _secondCollection.Length);
            return _firstCollection[firstIndex] + _secondCollection[secondIndex];
        }
    }
}

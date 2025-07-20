namespace MVC.Helper
{
    public static class ShuffleHelper
    {

        private static Random rng = new Random();

        public static List<T> Shuffle<T>(List<T> list)
        {
            return list.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}

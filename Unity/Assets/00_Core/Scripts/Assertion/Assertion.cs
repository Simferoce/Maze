namespace Game.Core
{
    public static class Assertion
    {
        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
                throw new AssertionException(message);
        }

        public static void IsNotNull(object condition, string message)
        {
            if (condition == null)
                throw new AssertionException(message);
        }
    }
}

namespace Game.Core
{
    public interface ILayout
    {
        public bool[,] Generate(int seed, int width, int height);
    }
}

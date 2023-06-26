namespace SandAndStonesEngine.Utils
{
    public interface IIndexGenerator
    {
        public ushort[] Points { get; }
        public void Generate();
    }
}

namespace SandAndStonesEngine.Animation
{
    public interface IAnimation
    {
        void Next(string param="");
        string GetCurrent();
    }
}

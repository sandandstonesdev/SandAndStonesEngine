namespace SandAndStonesEngine.Animation
{
    public interface IAnimation
    {
        bool Changed { get; set; }
        void Next(string param = "", int skipFrames = 0);
        string GetCurrent();
    }
}

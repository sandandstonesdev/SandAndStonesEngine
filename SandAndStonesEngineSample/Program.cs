using SandAndStonesEngine;
using SandAndStonesEngine.GameFactories;
using Veldrid.OpenGLBinding;

namespace SandAndStonesEngineSample
{
    class Program
    {
        static void Main()
        {
            var game = Factory.Instance.GetGame();
            game.Start();
            game.Loop();
            game.Stop();
        }
    }
}
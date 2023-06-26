using SandAndStonesEngine;

namespace SandAndStonesEngineSample
{
    class Program
    {
        static void Main()
        {
            var game = new Game();
            game.Start();
            game.Loop();
            game.Stop();
        }
    }
}
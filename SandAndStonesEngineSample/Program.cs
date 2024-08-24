using SandAndStonesEngine.GameFactories;

namespace SandAndStonesEngineSample
{
    class Program
    {
        static void Main()
        {
            Factory.Instance.GetGame().Start();
        }
    }
}
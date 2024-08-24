using Microsoft.Extensions.DependencyInjection;
using SandAndStonesEngine.GameFactories;

namespace SandAndStonesEngine
{
    public class Game : IDisposable
    {
        private static readonly Lazy<Game> lazyFactoryInstance = new(() => new Game());
        public static Game Instance => lazyFactoryInstance.Value;

        private bool disposedValue;

        public Game()
        {
        }

        public void Start()
        {
            var window = Startup.ServiceProvider.GetRequiredService<GameWindow>();
            window.Loop();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                var window = Startup.ServiceProvider.GetRequiredService<GameWindow>();
                window.Dispose();

                disposedValue = true;
            }
        }

        ~Game()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

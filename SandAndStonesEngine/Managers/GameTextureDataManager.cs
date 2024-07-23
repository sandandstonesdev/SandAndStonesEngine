using SandAndStonesEngine.Assets;

namespace SandAndStonesEngine.Managers
{
    internal class GameTextureDataManager
    {
        private static readonly Lazy<GameTextureDataManager> lazyInstance = new Lazy<GameTextureDataManager>(() => new GameTextureDataManager());
        public static GameTextureDataManager Instance => lazyInstance.Value;

        public List<GameTextureDataBase> TexturesData { get; } = new();

        public GameTextureDataManager()
        {

        }

        public void Add(GameTextureDataBase model)
        {
            TexturesData.Add(model);
        }

        public void AddRange(List<GameTextureDataBase> models)
        {
            TexturesData.AddRange(models);
        }
    }
}
using SandAndStonesEngine.Assets.Textures;

namespace SandAndStonesEngine.Managers
{
    public class GameTextureDataManager
    {
        public List<GameTextureDataBase> TexturesData { get; } = [];

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
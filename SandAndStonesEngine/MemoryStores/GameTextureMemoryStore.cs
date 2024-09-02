using SandAndStonesEngine.Assets.Textures;

namespace SandAndStonesEngine.MemoryStore
{
    public class GameTextureMemoryStore
    {
        public uint Count { get; private set; }
        public List<GameTextureDataBase> TexturesData { get; } = [];

        public GameTextureMemoryStore()
        {

        }

        public void Add(GameTextureDataBase model)
        {
            TexturesData.Add(model);
            Count++;
        }

        public void AddRange(List<GameTextureDataBase> models)
        {
            TexturesData.AddRange(models);
            Count++;
        }
    }
}
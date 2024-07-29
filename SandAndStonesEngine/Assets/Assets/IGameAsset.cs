using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels.Quads;

namespace SandAndStonesEngine.Assets.Assets
{
    public interface IGameAsset
    {
        public int Id { get; }
        List<IQuadModel> QuadModelList { get; }
        ITextureData GameTextureData { get; }
        void Init(int startX, int startY, int end, string textureName);
        void Update(double delta);
    }
}
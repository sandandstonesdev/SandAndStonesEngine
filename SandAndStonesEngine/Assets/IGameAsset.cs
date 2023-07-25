using SandAndStonesEngine.DataModels;

namespace SandAndStonesEngine.Assets
{
    public interface IGameAsset
    {
        List<IQuadModel> QuadModelList { get; }
        ITextureData GameTextureData { get; }
        void Init(int start, int end, QuadGrid quadGrid, string textureName);
        void Update(double delta);
    }
}
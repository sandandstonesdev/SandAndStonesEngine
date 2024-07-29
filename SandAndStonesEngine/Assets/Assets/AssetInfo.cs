using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Textures;
using System.Numerics;

namespace SandAndStonesEngine.Assets.Assets
{
    public class AssetInfo
    {
        public Vector3 StartPos { get; init; }
        public Vector3 EndPos { get; init; }
        public IAnimation Animation { get; init; }
        public List<TextureInfo> Textures { get; init; } = new List<TextureInfo>();

        private AssetInfo(int startPosX, int startPosY, int endPosX, int endPosY, IAnimation animation, TextureInfo texture)
        {
            StartPos = new Vector3(startPosX, startPosY, 0);
            EndPos = new Vector3(endPosX, endPosY, 0);
            Animation = animation;
            Textures.Add(texture);
        }

        public static AssetInfo Create2DAssetInfo(int startPosX, int startPosY, int endPosX, int endPosY, IAnimation animation, TextureInfo texture)
        {
            return new AssetInfo(startPosX, startPosY, endPosX, endPosY, animation, texture);
        }
    }
}

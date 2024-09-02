using System.Numerics;

namespace SandAndStonesEngine.Assets.DTOs
{
    public class AssetPosInfo
    {
        public Vector4 StartPos { get; init; }
        public Vector4 EndPos { get; init; }

        public AssetPosInfo(Vector2 startPos, Vector2 endPos)
        {
            StartPos = new Vector4(startPos, 0, 0);
            EndPos = new Vector4(endPos, 0, 0);
        }
        public AssetPosInfo(Vector4 startPos, Vector4 endPos)
        {
            StartPos = startPos;
            EndPos = endPos;
        }
    }
}

using System.Numerics;

namespace SandAndStonesEngine.Assets.DTOs
{
    public class AssetPosInfo
    {
        public Vector2 StartPos { get; init; }
        public Vector2 EndPos { get; init; }

        public AssetPosInfo(Vector2 startPos,Vector2 endPos)
        {
            StartPos = startPos;
            EndPos = endPos;
        }
    }
}

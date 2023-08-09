using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{
    public class SpriteTile : QuadModel
    {
        public SpriteTile(Vector2 gridQuadPosition, float quadScale, RgbaFloat color, uint assetId, int textureId) :
            base(new Vector3(gridQuadPosition, 0), quadScale, color, assetId, textureId)
        {

        }
    }
}

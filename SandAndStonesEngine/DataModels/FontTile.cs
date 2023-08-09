using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{
    public class FontTile : QuadModel
    {
        public FontTile(Vector2 gridQuadPosition, float fontSize, RgbaFloat color, uint assetId, int textureId) :
            base(new Vector3(gridQuadPosition, 1), fontSize=4.0f, color, assetId, textureId)
        {

        }
    }
}

using System.Numerics;
using SandAndStonesEngine.DataModels.Quads;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Tiles
{
    public class FontTile : QuadModel
    {
        public FontTile(Vector3 gridQuadPosition, float fontSize, RgbaFloat color, uint assetId, int textureId) :
            base(gridQuadPosition, fontSize, color, assetId, textureId)
        {

        }
    }
}

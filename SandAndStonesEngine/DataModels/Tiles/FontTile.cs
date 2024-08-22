using System.Numerics;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.DataModels.Quads;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Tiles
{
    public class FontTile : QuadModel
    {
        public FontTile(Vector2 screenPosition, Vector3 gridQuadPosition, float fontSize, RgbaFloat color, uint assetId, int textureId, TileType tileType) :
            base(screenPosition, gridQuadPosition, fontSize, color, assetId, textureId, tileType)
        {

        }
    }
}

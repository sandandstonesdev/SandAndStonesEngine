using System.Numerics;
using SandAndStonesEngine.DataModels.Quads;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Tiles
{
    public class BackgroundTile : QuadModel
    {
        public BackgroundTile(Vector2 screenId, Vector3 gridQuadPosition, float quadScale, RgbaFloat color, uint assetId, int textureId, TileType tileType) :
            base(screenId, gridQuadPosition, quadScale, color, assetId, textureId, tileType)
        {

        }
    }
}

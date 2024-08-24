using SandAndStonesEngine.DataModels.Quads;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Tiles
{
    public class BackgroundTile : QuadModel
    {
        public BackgroundTile(QuadData quadData, float quadScale, RgbaFloat color, uint assetId, int textureId, TileType tileType) :
            base(quadData, quadScale, color, assetId, textureId, tileType)
        {

        }
    }
}

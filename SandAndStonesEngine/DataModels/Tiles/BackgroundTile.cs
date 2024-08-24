using SandAndStonesEngine.DataModels.Quads;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Tiles
{
    public class BackgroundTile : QuadModel
    {
        public BackgroundTile(QuadData quadData, RgbaFloat color, uint assetId, int textureId, TileType tileType) :
            base(quadData, color, assetId, textureId, tileType)
        {

        }
    }
}

using SandAndStonesEngine.DataModels.Quads;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Tiles
{
    public class FontTile : QuadModel
    {
        public FontTile(QuadData quadData, RgbaFloat color, uint assetId, int textureId, TileType tileType) :
            base(quadData, color, assetId, textureId, tileType)
        {

        }
    }
}

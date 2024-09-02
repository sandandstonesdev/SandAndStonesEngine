using SandAndStonesEngine.DataModels.Quads;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Tiles
{
    public class CharacterSpriteTile : QuadModel
    {
        public CharacterSpriteTile(QuadData quadData, RgbaFloat color, uint assetId, int textureId, TileType tileType) :
            base(quadData, color, assetId, textureId, tileType)
        {

        }
    }
}

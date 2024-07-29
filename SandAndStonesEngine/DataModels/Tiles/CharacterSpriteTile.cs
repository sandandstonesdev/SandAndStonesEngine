using System.Numerics;
using SandAndStonesEngine.DataModels.Quads;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Tiles
{
    public class CharacterSpriteTile : QuadModel
    {
        public CharacterSpriteTile(Vector3 gridQuadPosition, float quadScale, RgbaFloat color, uint assetId, int textureId) :
            base(gridQuadPosition, quadScale, color, assetId, textureId)
        {

        }
    }
}

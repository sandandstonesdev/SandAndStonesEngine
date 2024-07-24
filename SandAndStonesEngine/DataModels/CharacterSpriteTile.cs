using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{
    public class CharacterSpriteTile : QuadModel
    {
        public CharacterSpriteTile(Vector2 gridQuadPosition, float quadScale, RgbaFloat color, uint assetId, int textureId) :
            base(new Vector3(gridQuadPosition, 0), quadScale, color, assetId, textureId)
        {

        }

        public override void Move(Vector2 movement)
        {
            base.Move(new Vector2(movement.X, movement.Y));
        }
    }
}

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

        public void ApplyMovement(Vector2 movement)
        {
            for (int i = 0; i < verticesPositions.Count(); i++)
            {
                verticesPositions[i].SetMovement(movement);
            }
        }
    }
}

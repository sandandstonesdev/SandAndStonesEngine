using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{
    public class BackgroundTile : QuadModel
    {
        public BackgroundTile(Vector2 gridQuadPosition, float layer, float quadScale, RgbaFloat color, uint assetId, int textureId) :
            base(new Vector3(gridQuadPosition, -1.0f/layer), quadScale, color, assetId, textureId)
        {

        }
    }
}

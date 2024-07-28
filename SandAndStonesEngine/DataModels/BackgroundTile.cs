using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{
    public interface IScrollableQuad
    {
        void Scroll(ScrollableViewport scrollableViewport);
    }

    public class BackgroundTile : QuadModel
    {
        public BackgroundTile(Vector2 gridQuadPosition, float layer, float quadScale, RgbaFloat color, uint assetId, int textureId) :
            base(new Vector3(gridQuadPosition, -layer/10), quadScale, color, assetId, textureId)
        {

        }

        public override void Move(Vector4 movement)
        {
            base.Move(movement);
        }
    }
}

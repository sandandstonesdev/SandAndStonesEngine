using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{
    public class BackgroundTile : QuadModel, IVisibleModel
    {
        public BackgroundTile(Vector2 gridQuadPosition, float layer, float quadScale, RgbaFloat color, uint assetId, int textureId) :
            base(new Vector3(gridQuadPosition, -layer/10), quadScale, color, assetId, textureId)
        {

        }

        public bool IsVisible(ScrollableViewport scrollableViewport)
        {
            return scrollableViewport.ContainsVertex(verticesPositions[0].Position)
            || scrollableViewport.ContainsVertex(verticesPositions[1].Position)
            || scrollableViewport.ContainsVertex(verticesPositions[2].Position)
            || scrollableViewport.ContainsVertex(verticesPositions[3].Position);
        }

        public void Scroll(ScrollableViewport scrollableViewport)
        {
            for (int i = 0; i < verticesPositions.Count(); i++)
            {
                verticesPositions[i].SetScroll(new Vector2(scrollableViewport.CartesianCoords.Item1, 
                                                           scrollableViewport.CartesianCoords.Item2));

            }
        }
    }
}

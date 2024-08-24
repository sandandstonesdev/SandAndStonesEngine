using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels.ScreenDivisions;
using SandAndStonesEngine.DataModels.Tiles;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Quads
{
    public class QuadModel : IQuadModel, IVisibleModel
    {
        private readonly int quadId;
        private readonly int quadBatchId;
        private readonly RgbaFloat color;
        private readonly Vector3[] quadPointsInGrid;
        private readonly Vector2[] quadTextureCoords;
        private readonly Vector2 screenInfo;
        private readonly ushort[] quadIndexesInGrid;
        private readonly int textureId;
        private readonly uint assetId;
        protected VertexDataFormat[] verticesPositions = new VertexDataFormat[4];

        public Vector2 ScreenInfo
        {
            get { return screenInfo; }
        }

        public VertexDataFormat[] VerticesPositions
        {
            get { return verticesPositions; }
        }

        public bool Visible
        {
            get;
        }

        public ushort[] QuadIndexes
        {
            get { return quadIndexesInGrid; }
        }
        public float scale;

        public QuadModel(QuadData quadData, float quadScale, RgbaFloat color, uint assetId, int textureId, TileType tileType)
        {
            screenInfo = quadData.ScreenPos;
            quadPointsInGrid = quadData.Points;
            quadIndexesInGrid = quadData.Indexes;
            quadTextureCoords = quadData.TextureCoords;
            quadId = quadData.Id;
            quadBatchId = quadData.BatchId;
            this.assetId = assetId;
            this.textureId = textureId;
            this.color = color;
            scale = quadScale;
        }

        public void Init(ScreenDivisionForQuads screenDivision)
        {
            var quadAbsoluteCoords = QuadGridCalculator.GetQuadAbsoluteCoords(screenDivision, quadPointsInGrid, scale);
            var screenOffset = new Vector3(screenInfo.X * 2.0f, screenInfo.Y * 2.0f, 0);

            verticesPositions[0] = new VertexDataFormat(screenOffset + quadAbsoluteCoords[0], color, quadTextureCoords[0], assetId, textureId);
            verticesPositions[1] = new VertexDataFormat(screenOffset + quadAbsoluteCoords[1], color, quadTextureCoords[1], assetId, textureId);
            verticesPositions[2] = new VertexDataFormat(screenOffset + quadAbsoluteCoords[2], color, quadTextureCoords[2], assetId, textureId);
            verticesPositions[3] = new VertexDataFormat(screenOffset + quadAbsoluteCoords[3], color, quadTextureCoords[3], assetId, textureId);
        }

        public virtual void Move(Vector4 movement)
        {
            for (int i = 0; i < verticesPositions.Length; i++)
            {
                verticesPositions[i].Position =
                    Vector4.Add(verticesPositions[i].Position, movement);
            }
        }

        public bool IsVisible(ScrollableViewport scrollableViewport)
        {
            return true;
            // Code below make rendering slow down (artifacts)
            //var screenOffset = new Vector4(screenInfo.X * 2.0f, screenInfo.Y * 2.0f, 0, 0);
            return scrollableViewport.ContainsVertex(verticesPositions[0].Position)
            || scrollableViewport.ContainsVertex(verticesPositions[1].Position)
            || scrollableViewport.ContainsVertex(verticesPositions[2].Position)
            || scrollableViewport.ContainsVertex(verticesPositions[3].Position);
        }
    }
}

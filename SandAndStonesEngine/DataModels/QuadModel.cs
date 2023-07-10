using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.GameCamera;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Numerics;
using System.Transactions;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{

    public class QuadModel
    {
        private QuadTransformations transformations;
        private QuadInputMotionMapper inputMotionMapper;
        private VertexDataFormat[] verticesPositions = new VertexDataFormat[0];
        private Vector2 relativePosition = new Vector2(-1.00f, 1.00f);
        private Vector2 quadSizeInCoord;

        private RgbaFloat color;
        private Vector2[] quadPointsInGrid;
        private Vector2[] quadTextureCoords;
        private ushort[] quadIndexesInGrid;
        private float quadScale;
        public VertexDataFormat[] VerticesPositions
        {
            get { return verticesPositions; }
        }
        public ushort[] QuadIndexes
        {
            get { return quadIndexesInGrid; }
        }
        public QuadModel(Vector2 gridQuadPosition, RgbaFloat color, QuadGrid quadGrid)
        {
            this.inputMotionMapper = new QuadInputMotionMapper();
            this.transformations = new QuadTransformations();
            QuadData quadData = quadGrid.GetQuadData((int)gridQuadPosition.X, (int)gridQuadPosition.Y);

            this.quadPointsInGrid = quadData.Points;
            this.quadIndexesInGrid = quadData.Indexes;
            this.quadTextureCoords = quadData.TextureCoords;
            this.quadScale = 0.5f;
            this.quadSizeInCoord = quadGrid.GetQuadSizeInCoordinates() * quadScale;
            this.color = color;
        }
        
        
        private Vector2 GetAbsoluteCoord(Vector2 quadGridPoint)
        {
            var scaledPoint = new Vector2(quadGridPoint.X, -quadGridPoint.Y) * new Vector2(quadSizeInCoord.X, quadSizeInCoord.Y);
            var res = relativePosition + scaledPoint;
            //res = scaledPoint;
            //Debug.WriteLine($"{ res.X} {res.Y}");
            return res;
        }
        public void Init()
        {
            var leftUpper = GetAbsoluteCoord(quadPointsInGrid[0]); //quadPointsInCoord[0];
            var leftDown = GetAbsoluteCoord(quadPointsInGrid[1]);
            var rightUpper = GetAbsoluteCoord(quadPointsInGrid[2]);
            var rightDown = GetAbsoluteCoord(quadPointsInGrid[3]);
            
            verticesPositions = new[]
            {
                new VertexDataFormat(new Vector3(leftDown, 0.0f), color, quadTextureCoords[0]),
                new VertexDataFormat(new Vector3(leftUpper, 0.0f), color, quadTextureCoords[1]),
                new VertexDataFormat(new Vector3(rightDown, 0.0f), color, quadTextureCoords[2]),
                new VertexDataFormat(new Vector3(rightUpper, 0.0f), color, quadTextureCoords[3]),
            };


        }

        private void Translate(Vector3 translation)
        {
            transformations.Translate(translation);
        }
        private void Rotate(Vector2 rotation)
        {
            Quaternion rotationQuaternion = new Quaternion(new Vector3(rotation, 0), 1);
            transformations.Rotate(rotationQuaternion);
        }
        private void Scale(Vector2 scale)
        {
            transformations.Scale(scale);
        }
        private Matrix4x4 GetTransformations()
        {
            return transformations.Get();
        }

        public void Update(float deltaSeconds)
        {
            var motionDir = inputMotionMapper.GetMotionDir();
            if (motionDir != Vector3.Zero)
            {
                Translate(motionDir);
            }

            Vector2 yawPitch = inputMotionMapper.GetYawPitchVector();       
            if (yawPitch != Vector2.Zero)
            {
                Rotate(yawPitch);
            }

            //verticesPositions[0] = verticesPositions[0].Position * GetTransformations();
        }
    }
}

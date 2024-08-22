using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.ScreenDivisions;
using SandAndStonesEngine.DataModels.Tiles;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace SandAndStonesEngineTests
{
    [TestClass]
    public class QuadTests
    {
        ScreenDivisionForQuads screenDivision;
        QuadGridManager quadGridManager;
        public QuadTests()
        {
            SetUp();
        }
        void SetUp()
        {
            int screenWidth = 400;
            int screenHeight = 400;
            int quadCount = 4;
            this.screenDivision = new ScreenDivisionForQuads(screenWidth, screenHeight, quadCount, quadCount);
            this.quadGridManager = QuadGridManager.Instance;
            quadGridManager.Init(screenDivision);
            quadGridManager.StartNewBatch();
        }

        [TestMethod]
        public void IndicesForQuadAreCorrect()
        {
            ushort[] indexesPattern1 = new ushort[] { 0, 1, 2, 1, 3, 2 };
            ushort[] indexesPattern2 = new ushort[] { 4, 5, 6, 5, 7, 6 };
            ushort[] indexesPattern3 = new ushort[] { 8, 9, 10, 9, 11, 10 };
            QuadData quadData1 = quadGridManager.GetQuadData(Vector2.Zero, new Vector3(1, 1, 0), TileType.Background);
            QuadData quadData2 = quadGridManager.GetQuadData(Vector2.Zero, new Vector3(1, 2, 0), TileType.Background);
            QuadData quadData3 = quadGridManager.GetQuadData(Vector2.Zero, new Vector3(1, 3, 0), TileType.Background);
            
            Assert.AreEqual(quadData1.Indexes.Length, indexesPattern1.Length);
            bool isEqual = Enumerable.SequenceEqual(quadData1.Indexes, indexesPattern1);
            Assert.IsTrue(isEqual);
            Assert.AreEqual(quadData2.Indexes.Length, indexesPattern2.Length);
            isEqual = Enumerable.SequenceEqual(quadData2.Indexes, indexesPattern2);
            Assert.IsTrue(isEqual);
            Assert.AreEqual(quadData3.Indexes.Length, indexesPattern3.Length);
            isEqual = Enumerable.SequenceEqual(quadData3.Indexes, indexesPattern3);
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void VerticesForQuadGridHasCorrectLayout()
        {
            const int verticesPerQuad = 4;
            QuadData quadData1 = quadGridManager.GetQuadData(Vector2.Zero, new Vector3(1, 1, 0), TileType.Background);

            Assert.AreEqual(quadData1.Points.Length, verticesPerQuad);
            bool AreLeftXsEqual = quadData1.Points[0].X == quadData1.Points[2].X;
            bool ArerRightXsEqual = quadData1.Points[1].X == quadData1.Points[3].X;
            bool AreUpYsEqual = quadData1.Points[0].Y == quadData1.Points[1].Y;
            bool ArerDownYsEqual = quadData1.Points[2].Y == quadData1.Points[3].Y;
            Assert.IsTrue(AreLeftXsEqual);
            Assert.IsTrue(ArerRightXsEqual);
            Assert.IsTrue(AreUpYsEqual);
            Assert.IsTrue(ArerDownYsEqual);
        }
    }
}
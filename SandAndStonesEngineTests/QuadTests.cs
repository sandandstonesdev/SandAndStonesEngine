using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandAndStonesEngine.DataModels;

namespace SandAndStonesEngineTests
{
    [TestClass]
    public class QuadTests
    {
        ScreenDivisionForQuads screenDivision;
        void SetUp()
        {
            int x = 50;
            int y = 50;
            int screenWidth = 400;
            int screenHeight = 400;
            int quadCount = 4;
            screenDivision = new ScreenDivisionForQuads(screenWidth, screenHeight, quadCount, quadCount);
        }

        [TestMethod]
        public void IndicesForQuadAreCorrect()
        {
            SetUp();
            ushort[] indexesPattern1 = new ushort[] { 0, 1, 2, 1, 3, 2 };
            ushort[] indexesPattern2 = new ushort[] { 4, 5, 6, 5, 7, 6 };
            ushort[] indexesPattern3 = new ushort[] { 8, 9, 10, 9, 11, 10 };
            QuadGrid quadGrid = new QuadGrid(screenDivision);
            QuadData quadData1 = quadGrid.GetQuadData(1, 1);
            QuadData quadData2 = quadGrid.GetQuadData(1, 2);
            QuadData quadData3 = quadGrid.GetQuadData(1, 3);
            
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
            SetUp();
            const int verticesPerQuad = 4;
            QuadGrid quadGrid = new QuadGrid(screenDivision);
            QuadData quadData1 = quadGrid.GetQuadData(1, 1);

            Assert.AreEqual(quadData1.Points.Length, verticesPerQuad);
            bool AreLeftXsEqual = quadData1.Points[0].X == quadData1.Points[1].X;
            bool ArerRightXsEqual = quadData1.Points[2].X == quadData1.Points[3].X;
            bool AreUpYsEqual = quadData1.Points[0].Y == quadData1.Points[2].Y;
            bool ArerDownYsEqual = quadData1.Points[1].Y == quadData1.Points[3].Y;
            Assert.IsTrue(AreLeftXsEqual);
            Assert.IsTrue(ArerRightXsEqual);
            Assert.IsTrue(AreUpYsEqual);
            Assert.IsTrue(ArerDownYsEqual);
        }
    }
}
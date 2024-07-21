using SandAndStonesEngine.DataModels;

namespace SandAndStonesEngineTests
{
    [TestClass]
    public class ScreenDivisionForQuadsTests
    {
        private ScreenDivisionForQuads screenDivision;
        private QuadGridManager quadGridManager;

        public ScreenDivisionForQuadsTests()
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
        public void ShouldGetCoordinateUnitsPerQuad()
        {
            // 4x4 Quads = 16
            // <-1, 1> 2.0Coord/4Quads = 0.5 Coord/Quad
            var res = screenDivision.GetCoordinateUnitsPerQuad();
            Assert.AreEqual(res.X, 0.5f);
            Assert.AreEqual(res.Y, 0.5f);
        }

        [TestMethod]
        public void ShouldGetCoordinateUnitsPerPixel()
        {
            // <-1, 1> czyli 2.0Coord/400Pix = 0,005Coord/Pix
            var res = screenDivision.GetCoordinateUnitsPerPixel();
            Assert.AreEqual(res.X, 0.005f);
            Assert.AreEqual(res.Y, 0.005f);
            Assert.AreEqual(res.Z, 0.005f);
        }

        [TestMethod]
        public void ShouldGetPixelUnitsPerQuad()
        {
            // 4x4 Quads = 16
            // <-1, 1> => 2.0Coord/4Quads = 0.5Coord/Quad
            // 0.5Coord/Quad / 0,005Coord/Pixel = 100 Pix/Quad
            var res = screenDivision.GetPixelUnitsPerQuad();
            Assert.AreEqual(res.X, 100);
            Assert.AreEqual(res.Y, 100);
        }
    }
}

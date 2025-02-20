using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.ScreenDivisions;
using System.Numerics;

namespace SandAndStonesEngineTests
{
    [TestClass]
    public class ScrollableViewportTests
    {
        ScreenQuadCalculator screenDivision;
        QuadGridManager quadGridManager;
        public ScrollableViewportTests()
        {
            SetUp();
        }
        void SetUp()
        {
            int screenWidth = 400;
            int screenHeight = 400;
            int quadCount = 4;
            this.screenDivision = new ScreenQuadCalculator(screenWidth, screenHeight, quadCount, quadCount);
            this.quadGridManager = new QuadGridManager(screenDivision);
            quadGridManager.StartNewBatch();
        }

        [TestMethod]
        public void ShouldNotContainVertexOutOfScrollableViewport()
        {
            var scrollableViewport = new ScrollableViewport(0, 0, 400, 400, screenDivision);
            scrollableViewport.Scroll(10, 10);
            bool result = scrollableViewport.ContainsVertex(new Vector4(1.25f, 1.25f, 0.0f, 0));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ShouldContainVertexInScrollableViewport()
        {
            var scrollableViewport = new ScrollableViewport(0, 0, 400, 400, screenDivision);
            scrollableViewport.Scroll(10, 10);
            bool result = scrollableViewport.ContainsVertex(new Vector4(0.25f, -0.25f, 0.0f, 0));
            Assert.IsTrue(result);
        }
    }
}

using SandAndStonesEngine.GameTextures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine;

namespace SandAndStonesEngineTests
{
    [TestClass]
    public class TextureTests
    {
        [TestMethod]
        public void ReadTextureBytesIsCorrect()
        {
            GameTexture texture1 = new GameTexture("wall.png");
            var textureBytes1 = texture1.GetImageBytes();
            texture1.WriteTextureToOutFile(textureBytes1);
            GameTexture texture2 = new GameTexture("wall.png");
            var textureBytes2 = texture2.GetImageBytes();
            CollectionAssert.AreEquivalent(textureBytes1, textureBytes2);
        }
    }
}

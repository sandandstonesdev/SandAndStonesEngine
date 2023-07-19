using SandAndStonesEngine.GameTextures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine;
using SandAndStonesEngine.Assets;

namespace SandAndStonesEngineTests
{
    [TestClass]
    public class TextureTests
    {
        [TestMethod]
        public void ReadTextureBytesIsCorrect()
        {
            GameTextureData texture1 = new GameTextureData(1, "wall.png");
            var textureBytes1 = texture1.GetImageBytes();
            texture1.WriteTextureToOutFile(textureBytes1);
            GameTextureData texture2 = new GameTextureData(2, "wall.png");
            var textureBytes2 = texture2.GetImageBytes();
            CollectionAssert.AreEquivalent(textureBytes1, textureBytes2);
        }
    }
}

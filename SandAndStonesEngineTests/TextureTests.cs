﻿using SandAndStonesEngine.Assets.Textures;

namespace SandAndStonesEngineTests
{
    [TestClass]
    public class TextureTests
    {
        [TestMethod]
        public void ReadTextureBytesIsCorrect()
        {
            GameTextureData texture1 = new GameTextureData(1, 1, "wall.png");
            var textureBytes1 = texture1.GetImageBytes(texture1.FileName);
            GameTextureData texture2 = new GameTextureData(2, 1, "wall.png");
            var textureBytes2 = texture2.GetImageBytes(texture1.FileName);

            string testText = "Test text";
            FontTextureData fontTexture1 = new FontTextureData(3, 1);
            var fontTextureBytes1 = fontTexture1.GetTextBitmap(testText);
            FontTextureData fontTexture2 = new FontTextureData(4, 1);
            var fontTextureBytes2 = fontTexture1.GetTextBitmap(testText);

            CollectionAssert.AreEquivalent(textureBytes1, textureBytes2);
            CollectionAssert.AreEquivalent(fontTextureBytes1, fontTextureBytes2);
        }
    }
}

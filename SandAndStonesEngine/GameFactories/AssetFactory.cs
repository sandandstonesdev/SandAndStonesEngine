using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using System.Numerics;
using Veldrid;
using TextureType = SandAndStonesEngine.Assets.Textures.TextureType;

namespace SandAndStonesEngine.GameFactories
{
    public class AssetFactory
    {
        private static readonly Lazy<AssetFactory> lazyFactoryInstance = new Lazy<AssetFactory>(() => new AssetFactory());
        public static AssetFactory Instance => lazyFactoryInstance.Value;
        private AssetFactory()
        {

        }

        public GameTextureDataBase CreateTexture(uint assetId, string fileName, TextureType type)
        {
            GameTextureDataBase texture = null!;
            if (type == TextureType.Standard)
                texture = new GameTextureData(assetId, fileName);
            else if (type == TextureType.Text)
                texture = new FontTextureData(assetId);
            return texture;
        }

        public IQuadModel CreateTile(QuadData quadData, float fontSize, RgbaFloat color, uint assetId, int textureId, TileType type)
        {
            IQuadModel quadModel = null!;

            if (type == TileType.Font)
                quadModel = new FontTile(quadData, fontSize, color, assetId, textureId, type);
            else if (type == TileType.StatusBarBackground)
                quadModel = new BackgroundTile(quadData, fontSize, color, assetId, textureId, type);
            else if (type == TileType.Background)
                quadModel = new BackgroundTile(quadData, fontSize, color, assetId, textureId, type);
            else if (type == TileType.Sprite)
                quadModel = new SpriteTile(quadData, fontSize, color, assetId, textureId, type);
            else if (type == TileType.Character)
                quadModel = new CharacterSpriteTile(quadData, fontSize, color, assetId, textureId, type);

            return quadModel;
        }
    }
}

using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels.Tiles;
using Veldrid;
using SandAndStonesEngine.DataModels.Quads;
using System.Numerics;
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

        public IQuadModel CreateTile(Vector2 screenPosition, Vector3 gridQuadPosition, float fontSize, RgbaFloat color, uint assetId, int textureId, TileType type)
        {
            IQuadModel quadModel = null!;
            
            if (type == TileType.Font)
                quadModel = new FontTile(screenPosition, gridQuadPosition, fontSize, color, assetId, textureId, type);
            else if (type == TileType.StatusBarBackground)
                quadModel = new BackgroundTile(screenPosition, gridQuadPosition, fontSize, color, assetId, textureId, type);
            else if (type == TileType.Background)
                quadModel = new BackgroundTile(screenPosition, gridQuadPosition, fontSize, color, assetId, textureId, type);
            else if (type == TileType.Sprite)
                quadModel = new SpriteTile(screenPosition, gridQuadPosition, fontSize, color, assetId, textureId, type);
            else if (type == TileType.Character)
                quadModel = new CharacterSpriteTile(screenPosition, gridQuadPosition, fontSize, color, assetId, textureId, type);

            return quadModel;
        }
    }
}

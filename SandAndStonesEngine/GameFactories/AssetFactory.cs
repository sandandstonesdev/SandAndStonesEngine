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

        public IQuadModel CreateTile(Vector3 gridQuadPosition, float fontSize, RgbaFloat color, uint assetId, int textureId, TileType type)
        {
            IQuadModel quadModel = null!;
            
            if (type == TileType.Font)
                quadModel = new FontTile(gridQuadPosition, fontSize, color, assetId, textureId);
            else if (type == TileType.Background)
                quadModel = new BackgroundTile(gridQuadPosition, fontSize, color, assetId, textureId);
            else if (type == TileType.Sprite)
                quadModel = new SpriteTile(gridQuadPosition, fontSize, color, assetId, textureId);
            else if (type == TileType.Character)
                quadModel = new CharacterSpriteTile(gridQuadPosition, fontSize, color, assetId, textureId);

            return quadModel;
        }
    }
}

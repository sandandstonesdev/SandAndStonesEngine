using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.DTOs;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.Clients;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using SandAndStonesEngine.MathModule;
using SandAndStonesLibrary.AssetConfig;
using SandAndStonesLibrary.Assets;
using Veldrid;
using TextureType = SandAndStonesEngine.Assets.Textures.TextureType;

namespace SandAndStonesEngine.GameFactories
{
    public class AssetFactory
    {

        public AssetFactory()
        {
        }

        public GameAssetBase CreateGameAsset(
            string name,
            AssetPosInfo posInfo,
            AssetFactory assetFactory,
            ScrollableViewport scrollableViewport,
            QuadGridManager gridManager,
            ViewTransformator viewTransformator,
            AssetBatchType assetBatchType,
            AssetType assetType,
            string text,
            string[] animationTextureFilenames,
            float depth,
            float scale,
            TextureInfo textureInfo)
        {
            GameAssetBase asset = null!;
            IAnimation animation = null!;

            if (assetType == AssetType.Background)
            {
                asset = new GameBackgroundAsset(name, scrollableViewport, assetBatchType, depth, scale);
                animation = new TextureAnimation(animationTextureFilenames);
            }
            else if (assetType == AssetType.CharacterSprite)
            {
                asset = new GameCharacterSpriteAsset(name, viewTransformator, assetBatchType, depth, scale);
                animation = new TextureAnimation(animationTextureFilenames);
            }
            else if (assetType == AssetType.Sprite)
            {
                asset = new GameSpriteAsset(name, assetBatchType, depth, scale);
                animation = new TextureAnimation(animationTextureFilenames);
            }
            else if (assetType == AssetType.FPSText)
            {
                asset = new GameFPSCounterTextAsset(name, assetBatchType, depth, scale);
                animation = new TextAnimation(text);
            }
            else if (assetType == AssetType.PointsText)
            {
                asset = new GamePointsCounterTextAsset(name, assetBatchType, depth, scale);
                animation = new TextAnimation(text);
            }
            else if (assetType == AssetType.Text)
            {
                asset = new GameTextAsset(name, assetBatchType, depth, scale);
                animation = new TextAnimation(text);
            }

            return asset!.Init
            (
                gridManager,
                AssetInfo.Create2DAssetInfo(assetFactory, gridManager.QuadCount,
                (int)posInfo.StartPos.X,
                (int)posInfo.StartPos.Y,
                (int)posInfo.EndPos.X,
                (int)posInfo.EndPos.Y,
                animation,
                textureInfo)
            );
        }

        public GameTextureDataBase CreateTexture(int id, uint assetId, string fileName, TextureType type)
        {
            GameTextureDataBase texture = null!;
            if (type == TextureType.Standard)
                texture = new GameTextureData(id, assetId, fileName);
            else if (type == TextureType.Text)
                texture = new FontTextureData(id, assetId);

            texture.Init();
            return texture;
        }

        public IQuadModel CreateTile(QuadGridManager gridManager, QuadData quadData, RgbaFloat color, uint assetId, int textureId, TileType type)
        {
            IQuadModel quadModel = null!;

            if (type == TileType.Font)
                quadModel = new FontTile(quadData, color, assetId, textureId, type);
            else if (type == TileType.StatusBarBackground)
                quadModel = new BackgroundTile(quadData, color, assetId, textureId, type);
            else if (type == TileType.Background)
                quadModel = new BackgroundTile(quadData, color, assetId, textureId, type);
            else if (type == TileType.Sprite)
                quadModel = new SpriteTile(quadData, color, assetId, textureId, type);
            else if (type == TileType.Character)
                quadModel = new CharacterSpriteTile(quadData, color, assetId, textureId, type);

            quadModel.Init(gridManager.screenDivision);

            return quadModel;
        }

        public IAsyncAssetReader CreateAssetReader(string path, bool externalService)
        {
            return externalService ? new InputAssetServiceReader(new HttpClient(), path) : new InputAssetReader(path);
        }
    }
}

using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.Assets.DTOs;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using SandAndStonesEngine.Managers;
using SandAndStonesEngine.MathModule;
using Veldrid;
using TextureType = SandAndStonesEngine.Assets.Textures.TextureType;

namespace SandAndStonesEngine.GameFactories
{
    public class AssetFactory
    {
        public readonly List<GameAssetBase> Assets = [];
        public List<IQuadModel> ModelData => quadModelManager.Models;
        public List<IQuadModel> StatusBarModels => quadModelManager.StatusBarModels;
        public List<GameTextureDataBase> TexturesData => gameTextureDataManager.TexturesData;

        private readonly QuadModelManager quadModelManager;
        private readonly GameTextureDataManager gameTextureDataManager;

        public AssetFactory(QuadModelManager quadModelManager, GameTextureDataManager gameTextureDataManager)
        {
            this.quadModelManager = quadModelManager;
            this.gameTextureDataManager = gameTextureDataManager;
        }

        public void Add(GameAssetBase asset)
        {
            Assets.Add(asset);
            if (asset.AssetBatchType == AssetBatchType.ClientRectBatch)
                quadModelManager.AddRange(asset.QuadModelList);
            if (asset.AssetBatchType == AssetBatchType.StatusBarBatch)
                quadModelManager.AddStatusBarModelsRange(asset.QuadModelList);

            gameTextureDataManager.Add(asset.GameTextureData);
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
            RgbaFloat color,
            string text,
            string[] animationTextureFilename,
            float depth,
            float scale)
        {
            GameAssetBase asset = null!;
            IAnimation animation = null!;

            if (assetType == AssetType.Background)
            {
                asset = new GameBackgroundAsset(name, scrollableViewport, assetBatchType, depth, scale);
                animation = new TextureAnimation(animationTextureFilename);
            }
            else if (assetType == AssetType.CharacterSprite)
            {
                asset = new GameCharacterSpriteAsset(name, viewTransformator, assetBatchType, depth, scale);
                animation = new TextureAnimation(animationTextureFilename);
            }
            else if (assetType == AssetType.Sprite)
            {
                asset = new GameSpriteAsset(name, assetBatchType, depth, scale);
                animation = new TextureAnimation(animationTextureFilename);
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
                TextureInfo.CreateTextureInfo(animationTextureFilename[0], color))
            );
        }

        public GameTextureDataBase CreateTexture(uint assetId, string fileName, TextureType type)
        {
            GameTextureDataBase texture = null!;
            if (type == TextureType.Standard)
                texture = new GameTextureData(assetId, fileName);
            else if (type == TextureType.Text)
                texture = new FontTextureData(assetId);

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
    }
}

﻿
using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using SandAndStonesEngine.GameFactories;
using System.Numerics;
using TextureType = SandAndStonesEngine.Assets.Textures.TextureType;

namespace SandAndStonesEngine.Assets.Assets
{
    public class GameStatusBarBackgroundAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.Background;
        public override AssetBatchType AssetBatchType { get; init; }
        public override bool IsText { get { return false; } }
        public override IAnimation Animation { get; set; }

        public GameStatusBarBackgroundAsset(string name, AssetBatchType assetBatchType, float depth, float scale = 1.0f) :
            base(name, depth, scale)
        {
            Id = IdManager.GetAssetId(AssetType);
            AssetBatchType = assetBatchType;
        }

        public override void Init(AssetInfo assetInfo)
        {
            Animation = assetInfo.Animation; 
            GameTextureData = AssetFactory.Instance.CreateTexture(Id, assetInfo.Textures[0].Name, TextureType.Standard);
            GameTextureData.Init();

            for (int i = (int)assetInfo.StartPos.X; i < assetInfo.EndPos.X; i++)
            {
                for (int j = (int)assetInfo.StartPos.Y; j < assetInfo.EndPos.Y; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    QuadModel quadModel = new BackgroundTile(positionInQuadCount, Scale, assetInfo.Textures[0].Color, Id, TextureId);
                    quadModel.Init();
                    QuadModelList.Add(quadModel);
                }
            }
        }

        public override void Update(long delta)
        {
            Animate();

            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
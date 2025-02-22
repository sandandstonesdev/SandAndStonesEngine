﻿using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using System.Numerics;
using SandAndStonesLibrary.AssetConfig;
using SandAndStonesLibrary.Assets;

namespace SandAndStonesEngine.Assets.Assets
{
    public class GameSpriteAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.Sprite;
        public override AssetBatchType AssetBatchType { get; init; }
        public override bool IsText { get { return false; } }

        public GameSpriteAsset(string name, AssetBatchType assetBatchType, float depth, float scale = 1.0f) :
            base(name, depth, scale)
        {
            Id = IdManager.GetAssetId(AssetType);
            AssetBatchType = assetBatchType;
        }

        public override GameAssetBase Init(QuadGridManager quadGridManager, AssetInfo assetInfo)
        {
            Animation = assetInfo.Animation;
            TextureInfo = assetInfo.Textures[0];

            for (int i = (int)assetInfo.StartPos.X; i < assetInfo.EndPos.X; i++)
            {
                for (int j = (int)assetInfo.StartPos.Y; j < assetInfo.EndPos.Y; j++)
                {
                    CreateAssetQuad(quadGridManager, assetInfo, new Vector2(0, 0), new Vector3(i, j, Depth), TileType.Sprite);
                }
            }

            return this;
        }

        public override void Update(long delta)
        {
            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

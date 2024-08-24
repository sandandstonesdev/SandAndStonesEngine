using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.MathModule;
using System.Numerics;
using TextureType = SandAndStonesEngine.Assets.Textures.TextureType;

namespace SandAndStonesEngine.Assets.Assets
{
    public class GameCharacterSpriteAsset : GameSpriteAsset
    {
        public override AssetType AssetType => AssetType.CharacterSprite;

        public override AssetBatchType AssetBatchType { get; init; }

        public override bool IsText => false;

        public override IAnimation Animation { get; set; }
        bool characterMoving = false;
        ViewTransformator viewTransformator;

        public GameCharacterSpriteAsset(string name, ViewTransformator viewTransformator, AssetBatchType assetBatchType, float depth, float scale = 1.0f) :
            base(name, AssetBatchType.ClientRectBatch, depth, scale)
        {
            Id = IdManager.GetAssetId(AssetType);
            AssetBatchType = assetBatchType;
            this.viewTransformator = viewTransformator;
            this.viewTransformator.PositionChanged += PositionChanged;
        }

        void PositionChanged(object? sender, EventArgs args)
        {
            characterMoving = true;
        }

        public override void Init(QuadGridManager quadGridManager, AssetInfo assetInfo)
        {
            Animation = assetInfo.Animation;
            GameTextureData = AssetFactory.Instance.CreateTexture(Id, assetInfo.Textures[0].Name, TextureType.Standard);
            GameTextureData.Init();

            for (int i = (int)assetInfo.StartPos.X; i < assetInfo.EndPos.X; i++)
            {
                for (int j = (int)assetInfo.StartPos.Y; j < assetInfo.EndPos.Y; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    //var screenPosition = new Vector2((int)i / 8, 0);
                    var quadData = quadGridManager.GetQuadData(new Vector2(0, 0), positionInQuadCount, TileType.Character);
                    var quadModel = AssetFactory.Instance.CreateTile(quadData, Scale, assetInfo.Textures[0].Color, Id, TextureId, TileType.Character);
                    quadModel.Init(quadGridManager.screenDivision);
                    QuadModelList.Add(quadModel);
                }
            }
        }

        public void Move(ViewTransformator viewTransformator)
        {
            QuadModelList.ForEach(x =>
            {
                x.Move(new Vector4(viewTransformator.TransformatorData.Movement, 0));
            });
        }

        public override void Update(long delta)
        {
            if (AssetBatchType == AssetBatchType.ClientRectBatch && characterMoving)
            {
                Move(viewTransformator);
                Animate();
                characterMoving = false;
            }

            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            viewTransformator.PositionChanged -= PositionChanged;
            base.Dispose(disposing);
        }
    }
}

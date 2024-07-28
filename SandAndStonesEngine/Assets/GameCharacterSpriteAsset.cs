using SandAndStonesEngine.Animation;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.MathModule;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameCharacterSpriteAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.CharacterSprite;

        public override AssetBatchType AssetBatchType { get; init; }

        public override bool IsText => false;

        public override IAnimation Animation { get; set; }
        bool characterMoving = false;
        ViewTransformator viewTransformator;

        public GameCharacterSpriteAsset(string name, ViewTransformator viewTransformator, RgbaFloat color, AssetBatchType assetBatchType, float depth, float scale = 1.0f) :
            base(name, color, depth, scale)
        {
            this.Id = IdManager.GetAssetId(AssetType);
            this.AssetBatchType = assetBatchType;
            this.viewTransformator = viewTransformator;
            this.viewTransformator.PositionChanged += PositionChanged;
        }

        void PositionChanged(object? sender, EventArgs args)
        {
            characterMoving = true;
        }

        public override void Init(int startX, int startY, int endX, int endY, string textureName)
        {
            GameTextureData = new GameTextureData(Id, textureName);
            GameTextureData.Init();

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    QuadModel quadModel = new CharacterSpriteTile(new Vector2(positionInQuadCount.X, positionInQuadCount.Y), Scale, Color, Id, TextureId);
                    quadModel.Init();
                    QuadModelList.Add(quadModel);
                }
            }
        }

        public void Move(ViewTransformator viewTransformator)
        {
            QuadModelList.ForEach(x =>
            {
                x.Move(new Vector4(
                    viewTransformator.TransformatorData.Movement.X, viewTransformator.TransformatorData.Movement.Y, 0, 0));
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
            this.viewTransformator.PositionChanged -= PositionChanged;
            base.Dispose(disposing);
        }
    }
}

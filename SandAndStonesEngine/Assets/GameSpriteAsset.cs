using System.Numerics;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Utils;
using Veldrid;
using static System.Formats.Asn1.AsnWriter;

namespace SandAndStonesEngine.Assets
{
    public class GameSpriteAsset : GameAssetBase
    {
        protected override AssetType AssetType => AssetType.Sprite;
        public override bool IsText { get { return false; } }
        public GameSpriteAsset(string name, RgbaFloat color, float depth, float scale = 1.0f) : 
            base(name, color, depth, scale)
        {
            this.Id = IdManager.GetAssetId(AssetType);
        }

        public override void Init(int startX, int startY, int endX, int endY, string textureName)
        {
            SetParam(textureName);
            GameTextureData = new GameTextureData(Id, textureName);
            GameTextureData.Init();

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    QuadModel quadModel = new SpriteTile(new Vector2(positionInQuadCount.X, positionInQuadCount.Y), Scale, Color, Id, TextureId);
                    quadModel.Init();
                    QuadModelList.Add(quadModel);
                }
            }
        }


        public override void SetParam(object param)
        {
            base.SetParam(param);
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

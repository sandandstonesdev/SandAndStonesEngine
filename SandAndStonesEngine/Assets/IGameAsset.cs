﻿using SandAndStonesEngine.DataModels;

namespace SandAndStonesEngine.Assets
{
    public interface IGameAsset
    {
        List<IQuadModel> QuadModelList { get; }
        ITextureData GameTextureData { get; }
        void Init(int startX, int startY, int end, QuadGrid quadGrid, string textureName);
        void Update(double delta);
    }
}
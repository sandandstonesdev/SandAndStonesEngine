﻿using System.Numerics;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.DataModels.Quads;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Tiles
{
    public class CharacterSpriteTile : QuadModel
    {
        public CharacterSpriteTile(Vector2 screenPosition, Vector3 gridQuadPosition, float quadScale, RgbaFloat color, uint assetId, int textureId, TileType tileType) :
            base(screenPosition, gridQuadPosition, quadScale, color, assetId, textureId, tileType)
        {

        }
    }
}

#version 450

layout(set = 1, binding = 0) uniform ProjectionBuffer
{
    mat4 Projection;
};

layout(set = 1, binding = 1) uniform ViewBuffer
{
    mat4 View;
};

layout(set = 2, binding = 0) uniform WorldBuffer
{
    mat4 World;
};


layout(location = 0) in vec4 Position;
layout(location = 1) in vec4 Color;
layout(location = 2) in vec2 TexCoords;
layout(location = 3) in int TextureId;
layout(location = 4) in uint AssetId;
layout(location = 5) in vec2 ScrollPosition;

layout(location = 0) out vec4 fsin_Color;
layout(location = 1) out vec3 fsin_TexCoords;

void main()
{
    vec4 objectPosition = Position;
    
    uint assetType = AssetId >> 29;

    if (assetType == 1) // BG
    {
        gl_Position = objectPosition + vec4(vec2(ScrollPosition.x/20, ScrollPosition.y/20), 0, 0);
    }
    else if (assetType == 2) // SPR
    {
        vec4 worldPosition = World * objectPosition;
        vec4 viewPosition = View * worldPosition;
        vec4 clipPosition =  Projection * viewPosition;
        gl_Position = clipPosition;
    }
    else if (assetType == 4) // TXT
    {
        gl_Position = objectPosition;
    }
    
    fsin_Color = Color;
    fsin_TexCoords = vec3(TexCoords, TextureId);
}
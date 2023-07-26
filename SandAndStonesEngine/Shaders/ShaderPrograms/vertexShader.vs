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

layout(location = 0) out vec4 fsin_Color;
layout(location = 1) out vec3 fsin_TexCoords;

void main()
{
    vec4 objectPosition = vec4(Position);
    
    if (TextureId < 3)
    {
        vec4 worldPosition = World * objectPosition;
        vec4 viewPosition = View * worldPosition;
        vec4 clipPosition =  Projection * viewPosition;
        gl_Position = clipPosition;
        fsin_Color = Color;
    }
    else
    {
        gl_Position = objectPosition;
        fsin_Color = vec4(1, 1, 1, 1);
    }
    
    fsin_TexCoords = vec3(TexCoords, TextureId);
}
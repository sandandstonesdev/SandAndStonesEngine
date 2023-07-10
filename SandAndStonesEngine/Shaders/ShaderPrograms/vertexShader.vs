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

layout(location = 0) out vec4 fsin_Color;
layout(location = 1) out vec2 fsin_texCoords;

void main()
{
    vec4 objectPosition = vec4(Position);
    vec4 worldPosition = World * objectPosition;
    vec4 viewPosition = View * worldPosition;
    vec4 clipPosition =  Projection * viewPosition;
    gl_Position = clipPosition;
    //gl_Position = objectPosition;
    fsin_Color = Color;
    fsin_texCoords = TexCoords;
}
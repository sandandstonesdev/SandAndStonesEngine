using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public class VertexInstanceLayout
    {
        const string positionName = "Position";
        const string colorName = "Color";
        const string texCoordsName = "TexCoords";
        const string texIdName = "TextureId";

        public readonly VertexLayoutDescription VertexLayoutPrototype = new VertexLayoutDescription(
                new VertexElementDescription(positionName, VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4),
                new VertexElementDescription(colorName, VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4),
                new VertexElementDescription(texCoordsName, VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                new VertexElementDescription(texIdName, VertexElementSemantic.TextureCoordinate, VertexElementFormat.Int1)
                );

        public VertexLayoutDescription Layout
        {
            get { return VertexLayoutPrototype; }
        }
    }
}

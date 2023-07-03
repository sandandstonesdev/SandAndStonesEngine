using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    internal class VertexLayouts
    {
        const string positionName = "Position";
        const string colorName = "Color";
        const string texCoordsName = "TexCoords";

        public VertexLayoutDescription VertexLayoutPrototype = new VertexLayoutDescription(
                new VertexElementDescription(positionName, VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float3),
                new VertexElementDescription(colorName, VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4),
                new VertexElementDescription(texCoordsName, VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2));
                

        private VertexLayoutDescription[] Layouts;

        public VertexLayouts(int vertexLayoutsToCreate)
        {
            List<VertexLayoutDescription> VertexLayoutList = new List<VertexLayoutDescription>();
            for (int i = 0; i < vertexLayoutsToCreate; i++)
            {
                VertexLayoutList.Add(VertexLayoutPrototype);
            }

            Layouts = VertexLayoutList.ToArray();
        }

        public VertexLayoutDescription[] GetAll()
        {
            return Layouts;
        }
    }
}

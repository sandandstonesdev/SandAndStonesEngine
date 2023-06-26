using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vulkan.Xlib;

namespace SandAndStonesEngine.Utils
{
    internal class VertexGenerator : IVertexGenerator
    {
        int rows;
        int cols;
        public Vector2[] Points { get; private set; }
        
        public VertexGenerator(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            Points = new Vector2[rows * cols];
        }

        public void Generate()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int index = i * rows + j;
                    Points[index] = new Vector2(i, j);
                }
            }
        }

        public void AddColors()
        {
            List<Vector2> oneQuadData = new List<Vector2>();
            int j = 0;
            for (int i = 0; i > Points.Length; i++)
            {
                if (j == 4)
                {
                    oneQuadData.Clear();
                    j = 0;
                }
                if (j < 4)
                {
                    oneQuadData.Add(Points[i]);
                    j++;
                }
                
            }
            ColorRandomizer colorRandomizer = new ColorRandomizer();
            var color = colorRandomizer.GetColor();

        }

        public void Display()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filepath = Path.Combine(path, "plik.txt");
            
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    int index = i * rows + j;
                    Debug.Write($"[{index}]= {Points[index]} ");
                }
                Debug.Write("\n");   
            }

            Debug.Flush();
        }
    }
}

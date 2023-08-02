using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.Utils
{
    public class TriangleStripIndexGenerator : IIndexGenerator // Indexes for triangle strips (not uses at this moment)
    {
        int rows;
        int cols;
        public ushort[] Points { get; private set; }
        // Pattern [1, 6, 2, 7, 3, 8, 4, 9, 5, 10, ..., 6, 11, 7, 12, 8, 13, 9, 14, 10, 15]
        public TriangleStripIndexGenerator(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            Points = new ushort[rows * cols];
        }

        public void Generate()
        {
            List<int> indices = new List<int>(); 
            for (int i = 0; i < rows; i++)
            {
                if (i != 0) indices.Add((i) * rows); // col=1 row=next (triangle degeneration)
                for (int j = 0; j < cols; j++)
                {
                    int computedIndex1 = i * cols + j; // col=j row=i
                    int computedIndex2 = (i + 1) * cols + j; // col=j row=i+1
                    indices.Add(i * cols + j);
                    indices.Add((i + 1) * cols + j);
                }
                // If i is not last row (triangle degeneration)
                if (i < rows - 2)
                {
                    indices.Add((i + 1) * rows + (cols - 1)); // Last vertex from next row
                }
            }

            Points = Array.ConvertAll(indices.ToArray(), Convert.ToUInt16);
        }

        public void Display()
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Debug.Write($"{Points[i]} ");
            }

            Debug.Flush();
        }
    }
}

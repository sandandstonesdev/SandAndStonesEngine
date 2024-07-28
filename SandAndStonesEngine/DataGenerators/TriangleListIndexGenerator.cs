namespace SandAndStonesEngine.Utils
{
    public class TriangleListIndexGenerator : IIndexGenerator
    {
        const int quadVerticesBatchIdCount = 4;
        int rows;
        int cols;
        int quadId;
        public ushort[] Points { get; private set; }
        // Pattern [1, 6, 2, 7, 3, 8, 4, 9, 5, 10, ..., 6, 11, 7, 12, 8, 13, 9, 14, 10, 15]

        ushort[] indexesPattern = new ushort[] { 0, 1, 2, 1, 3, 2 };
        public TriangleListIndexGenerator(int quadId, int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            this.quadId = quadId;
            Points = new ushort[rows * cols];
        }
        public void Generate()
        {
            ushort[] indices = indexesPattern.Select(x => (ushort)(x + (quadId * quadVerticesBatchIdCount))).ToArray();
            Points = indices;
        }
    }
}

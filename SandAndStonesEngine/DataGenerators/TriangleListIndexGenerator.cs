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

        ushort[] indexesPattern = new ushort[] { 0, 1, 2, 1, 3, 2 }; // 1 (For idx 3 2 1 0)
        //ushort[] indexesPattern = new ushort[] { 3, 2, 1, 2, 0, 1 }; // upside down 1
        
        public TriangleListIndexGenerator(int quadId)
        {
            this.quadId = quadId;
            Points = new ushort[indexesPattern.Length];
        }
        public void Generate()
        {
            var indexPatternLength = indexesPattern.Length;
            for (int i = 0; i < indexPatternLength; i++)
            {
                Points[i] = (ushort)(indexesPattern[i] + (quadId * quadVerticesBatchIdCount));
            }
        }
    }
}

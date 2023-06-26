namespace SandAndStonesEngine.Buffers
{
    public class IndicesGenerator
    {
        int indicesBatchCount; // Every batch is 4 indices
        public IndicesGenerator(int indicesBatchCount)
        {
            this.indicesBatchCount = indicesBatchCount;
        }
        public ushort[] GenerateUShortTable()
        {
            List<ushort> indices = new List<ushort>();
            for (int i = 0; i < indicesBatchCount; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ushort u = (ushort)((i * 4) + j);
                    indices.Add(u);
                }
            }

            return indices.ToArray();
        }
    }
}

namespace SandAndStonesEngine.Scrolling
{
    public class TileData
    {
        public byte[] PixelData;

        public TileData(byte[] pixelData)
        {
            PixelData = pixelData;
        }
    }

    public class TilesHeapElement
    {
        List<TilesHeapElement> Subtiles = new List<TilesHeapElement>(new TilesHeapElement[4]);
        TileData TileData;
        bool isLeaf = false;
        int vieportWidth;
        int viewportHeight;
        int chunkWidth;
        int chunkHeight;


        public TilesHeapElement()
        {

        }

        private void AddTileData(TileData tileData)
        {
            isLeaf = true;
            //tileData.PixelData
        }
    }
}

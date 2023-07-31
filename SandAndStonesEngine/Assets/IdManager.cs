namespace SandAndStonesEngine.Assets
{
    public class IdManager
    {
        static object lockObject = new object(); 
        static int AssetId = 0;
        public static int GetAssetId()
        {
            lock (lockObject)
            {
                return AssetId++;
            }
        }
    }
}

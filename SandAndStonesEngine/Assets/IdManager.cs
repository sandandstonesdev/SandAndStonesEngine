using System.Diagnostics;

namespace SandAndStonesEngine.Assets
{
    public class IdManager
    {
        static object lockObject = new object(); 
        static int AssetId = 0;
        static int QuadId = 0;
        static int TextureId = 0;
        public static int GetAssetId()
        {
            lock (lockObject)
            {
                return AssetId++;
            }
        }

        public static int GetTextureId()
        {
            lock (lockObject)
            {
                return TextureId++;
            }
        }
    }
}

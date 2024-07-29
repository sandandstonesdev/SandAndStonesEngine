using System.Diagnostics;
using SandAndStonesEngine.Assets.Assets;

namespace SandAndStonesEngine.Assets
{
    public class IdManager
    {
        static object lockObject = new object(); 
        static int AssetId = 0;
        static int QuadBatchId = 0;
        static int QuadId = 0;
        static int TextureId = 0;

        public static uint GetAssetId(AssetType type)
        {
            lock (lockObject)
            {
                uint retAssetId = (uint)AssetId++;
                retAssetId |= (uint)type << 28;
                return retAssetId++;
            }
        }

        public static int GetTextureId()
        {
            lock (lockObject)
            {
                return TextureId++;
            }
        }

        public static int GetNextQuadBatchId()
        {
            lock (lockObject)
            {
                QuadId = 0;
                return QuadBatchId++;
            }
        }
        public static int GetNextQuadId()
        {
            lock (lockObject)
            {
                return QuadId++;
            }
        }
    }
}

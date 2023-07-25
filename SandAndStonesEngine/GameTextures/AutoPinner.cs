using System.Runtime.InteropServices;
using Vortice.D3DCompiler;

namespace SandAndStonesEngine.GameTextures
{
    public class AutoPinner : IDisposable
    {
        GCHandle _pinnedArray;
        int Size;
        public AutoPinner(Object obj)
        {
            var arr = obj as byte[];
            Size = arr.Length;
            _pinnedArray = GCHandle.Alloc(obj, GCHandleType.Pinned);
        }
        public static implicit operator IntPtr(AutoPinner ap)
        {
            return ap._pinnedArray.AddrOfPinnedObject();
        }

        public byte[] GetArray()
        {
            var obj = _pinnedArray.Target as byte[];
            //Span<byte> byteArray = new Span<byte>(_pinnedArray., Size);
            return obj.ToArray();
        }
        public void Dispose()
        {
            _pinnedArray.Free();
        }
    }
}

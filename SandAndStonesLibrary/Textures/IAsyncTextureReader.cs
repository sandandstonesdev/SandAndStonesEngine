namespace SandAndStonesLibrary.Textures
{
    public interface IAsyncTextureReader
    {
        Task<InputTexture> ReadTextureAsync();
    }
}

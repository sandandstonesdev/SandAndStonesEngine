namespace InputTextureService.TextureConfig
{
    public interface IAsyncTextureReader
    {
        Task<InputTexture> ReadTextureAsync();
    }
}

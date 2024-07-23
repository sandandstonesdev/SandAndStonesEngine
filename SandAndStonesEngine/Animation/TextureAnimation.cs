
namespace SandAndStonesEngine.Animation
{
    public class TextureAnimation : IAnimation
    {
        private int selectedTexture = 0;
        private List<string> TextureFilenames { get; init; } = new();
        public TextureAnimation(params string[] textureFilenames)
        {
            TextureFilenames = textureFilenames.ToList();
        }

        public void Next(string param)
        {
            if (selectedTexture < TextureFilenames.Count - 1)
            {
                selectedTexture++;
            }
            else
            {
                selectedTexture = 0;
            }
        }

        public string GetCurrent()
        {
            return TextureFilenames[selectedTexture];
        }
    }
}

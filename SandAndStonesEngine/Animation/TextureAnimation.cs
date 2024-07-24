
namespace SandAndStonesEngine.Animation
{
    public class TextureAnimation : IAnimation
    {
        private int skipFramesCounter = 0;
        private int selectedTexture = 0;
        private int lastSelectedTexture = 0;

        private List<string> TextureFilenames { get; init; } = new();
        public bool Changed { get; set; }
        public TextureAnimation(params string[] textureFilenames)
        {
            TextureFilenames = textureFilenames.ToList();
        }

        public void Next(string param, int skipFrames = 0)
        {
            if (skipFrames > 1 && skipFramesCounter++ % skipFrames != 0)
                return;

            if (selectedTexture < TextureFilenames.Count - 1)
            {
                selectedTexture++;
            }
            else
            {
                selectedTexture = 0;
            }

            if (lastSelectedTexture != selectedTexture)
            {
                lastSelectedTexture = selectedTexture;
                Changed = true;
            }
        }

        public string GetCurrent()
        {
            return TextureFilenames[selectedTexture];
        }
    }
}

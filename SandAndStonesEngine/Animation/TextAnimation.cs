namespace SandAndStonesEngine.Animation
{
    public class TextAnimation : IAnimation
    {
        private int skipFramesCounter = 0;
        private string Text { get; set; }
        public bool Changed { get; set; }

        public TextAnimation(string text = "")
        {
            Next(text);
        }

        public void Next(string textToSet, int skipFrames = 0)
        {
            if (skipFrames > 1 && skipFramesCounter++ % skipFrames != 0)
                return;

            if (Text != textToSet)
            {
                Text = textToSet;
                Changed = true;
            }
        }

        public string GetCurrent()
        {
            return Text;
        }
    }
}

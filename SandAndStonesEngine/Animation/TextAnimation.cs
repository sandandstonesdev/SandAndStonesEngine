using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.Animation
{
    public class TextAnimation : IAnimation
    {
        private string Text { get; set; }
        public TextAnimation(string text = "")
        {
            Next(text);
        }

        public void Next(string textToSet)
        {
            Text = textToSet;
        }

        public string GetCurrent()
        {
            return Text;
        }
    }
}

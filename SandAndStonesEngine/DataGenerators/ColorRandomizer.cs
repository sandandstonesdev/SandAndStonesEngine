using Veldrid;

namespace SandAndStonesEngine.Utils
{
    public class ColorRandomizer
    {
        Dictionary<int, RgbaFloat> colorsToRandomize = new Dictionary<int, RgbaFloat>()
            {
                { 0, RgbaFloat.Red},
                { 1, RgbaFloat.Green},
                { 2, RgbaFloat.Blue },
                { 3, RgbaFloat.Cyan },
                { 4, RgbaFloat.Yellow },
                { 5, RgbaFloat.CornflowerBlue },
                { 6, RgbaFloat.LightGrey},
                { 7, RgbaFloat.Pink},
                { 8, RgbaFloat.Orange},
            };
        Random random;
        public ColorRandomizer()
        {
            TimeSpan epochDiff = DateTime.Now - DateTime.UnixEpoch;
            random = new Random(epochDiff.Milliseconds);
        }
        public RgbaFloat GetColor()
        {
            int colorKey = random.Next() % (colorsToRandomize.Count - 1);
            return colorsToRandomize[colorKey];
        }
    }
}

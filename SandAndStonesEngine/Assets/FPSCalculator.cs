namespace SandAndStonesEngine.Assets
{
    public class FPSCalculator
    {
        private static readonly Lazy<FPSCalculator> lazyInstance = new Lazy<FPSCalculator>(() => new FPSCalculator());
        public static FPSCalculator Instance => lazyInstance.Value;

        int sampleCount;
        LinkedList<long> deltasBuffer = new LinkedList<long>();
        UpdateFPSScheduler updateScheduler = new UpdateFPSScheduler();
        public FPSCalculator(int sampleCount = 10)
        {
            this.sampleCount = sampleCount;
            for (int i = 0; i < sampleCount; i++)
            {
                deltasBuffer.AddFirst(0);
            }
        }

        public void AddNextDelta(long delta)
        {
            deltasBuffer.RemoveLast();
            deltasBuffer.AddFirst(delta);
        }

        public bool CanDoUpdate(long delta)
        {
            return updateScheduler.DoUpdate(delta);
        }

        public double GetResult()
        {
            long result = 0;
            long sampleSum = 0;
            foreach (var deltaSample in deltasBuffer)
            {
                sampleSum += deltaSample;
            }

            result = sampleSum != 0 ? (sampleCount * 1000) / sampleSum : 0;
            return result;
        }

        public string GetFormatedResult()
        {
            int fps = (int)GetResult();
            string text = $"FPS: {fps}";
            return text;
        }
    }
}

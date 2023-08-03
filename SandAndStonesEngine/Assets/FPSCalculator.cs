namespace SandAndStonesEngine.Assets
{
    public class FPSCalculator
    {
        int sampleCount;
        LinkedList<long> deltasBuffer = new LinkedList<long>();
        UpdateFPSScheduler updateScheduler = new UpdateFPSScheduler();
        public FPSCalculator(int sampleCount)
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
            foreach(var deltaSample in deltasBuffer)
            {
                sampleSum += deltaSample; 
            }
            
            result = (sampleCount * 1000) / sampleSum;
            return result;
        }
    }
}

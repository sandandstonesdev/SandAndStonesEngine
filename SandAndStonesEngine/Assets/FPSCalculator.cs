namespace SandAndStonesEngine.Assets
{
    public class FPSCalculator
    {
        int sampleCount;
        LinkedList<double> deltasBuffer = new LinkedList<double>();
        UpdateFPSScheduler updateScheduler = new UpdateFPSScheduler();
        public FPSCalculator(int sampleCount)
        {
            this.sampleCount = sampleCount;
            for (int i = 0; i < sampleCount; i++)
            {
                deltasBuffer.AddFirst(0);
            }
        }

        public void AddNextDelta(double delta)
        {
            deltasBuffer.RemoveLast();
            deltasBuffer.AddFirst(delta);
        }

        public bool CanDoUpdate(double delta)
        {
            return updateScheduler.DoUpdate(delta);
        }

        public double GetResult()
        {
            double result = 0;
            double sampleSum = 0;
            foreach(var deltaSample in deltasBuffer)
            {
                sampleSum += deltaSample; 
            }
            
            result = ((double)sampleCount / (double)sampleSum) * 1000;
            return result;
        }
    }
}

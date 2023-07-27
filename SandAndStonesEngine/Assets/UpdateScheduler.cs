namespace SandAndStonesEngine.Assets
{
    public class UpdateFPSScheduler
    {
        double totalDelta = 0;
        double maxTotalDelta = 0;
        public UpdateFPSScheduler(int maxTotalDelta = 1000)
        {
            this.maxTotalDelta = 1000;
        }

        public bool DoUpdate(double delta)
        {
            totalDelta += delta;
            if (totalDelta >= maxTotalDelta)
            {
                totalDelta = 0;
                return true;
            }
            return false;
        }
    }
}

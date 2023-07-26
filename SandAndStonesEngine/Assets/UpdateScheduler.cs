namespace SandAndStonesEngine.Assets
{
    public class UpdateScheduler
    {
        double totalDelta = 0;
        double maxTotalDelta = 0;
        public UpdateScheduler(int maxTotalDelta = 1000)
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

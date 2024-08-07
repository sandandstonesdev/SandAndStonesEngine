﻿namespace SandAndStonesEngine.Assets
{
    public class UpdateFPSScheduler
    {
        double totalDelta = 0;
        double maxTotalDelta = 0;
        public UpdateFPSScheduler(int maxTotalDelta = 1000)
        {
#if DEBUG
            this.maxTotalDelta = 500;
#else
            this.maxTotalDelta = 500;
#endif
        }

        public bool DoUpdate(long delta)
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

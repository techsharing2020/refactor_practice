using System;

namespace Drama.Models
{
    public abstract class PerformanceCalculator
    {
        public abstract decimal GetAmount(int audience);

        public virtual decimal GetCredits(int audience)
        {
            return Math.Max(audience - 30, 0m);
        }
    }
}
using System;

namespace Drama.Models
{
    public class ComedyCalculator : PerformanceCalculator
    {
        public override decimal GetAmount(int audience)
        {
            decimal result = 30000;
            if (audience > 20)
            {
                result += 10000 + 500 * (audience - 20);
            }

            result += 300 * audience;
            return result;
        }

        public override decimal GetCredits(int audience)
        {
            return base.GetCredits(audience) + Math.Floor(audience / 5m);
        }
    }
}
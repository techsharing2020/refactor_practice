namespace Drama.Models
{
    public class TragedyCalculator : PerformanceCalculator
    {
        public override decimal GetAmount(int audience)
        {
            decimal result = 40000;
            if (audience > 30)
            {
                result += 1000 * (audience - 30);
            }

            return result;
        }
    }
}
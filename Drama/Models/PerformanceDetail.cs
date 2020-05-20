namespace Drama.Models
{
    public class PerformanceDetail : Performance
    {
        public Play Play { get; set; }
        public decimal Amount { get; set; }
        public decimal Credits { get; set; }
    }
}
using System.Collections.Generic;

namespace Drama.Models
{
    public class StatementData
    {
        public string Customer { get; set; }
        public IEnumerable<PerformanceDetail> PerformanceDetails { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VolumeCredits { get; set; }
    }
}
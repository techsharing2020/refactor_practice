using System.Collections.Generic;

namespace Drama.Models
{
    public class StatementData
    {
        public string Customer { get; set; }
        public IEnumerable<PerformanceDetail> PerformanceDetails { get; set; }
    }
}
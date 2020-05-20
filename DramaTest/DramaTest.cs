using System.Collections.Generic;
using Drama;
using Drama.Models;
using NUnit.Framework;

namespace DramaTest
{
    public class DramaTest
    {
        [Test]
        public void Test1()
        {
            var playDic = new Dictionary<string, Play>
            {
                {"hamlet", new Play{Name = "Hamlet", Type = PlayType.Tragedy} },
                {"as-like", new Play{Name = "As You Like It", Type = PlayType.Comedy} },
                {"othello", new Play{Name = "Othello", Type = PlayType.Tragedy} }
            };

            var invoice = new Invoice
            {
                Customer = "BigCo",
                Performances = new List<Performance>
                {
                    new Performance{PlayID = "hamlet", Audience = 55},
                    new Performance{PlayID = "as-like", Audience = 35},
                    new Performance{PlayID = "othello", Audience = 40}
                }
            };

            var actual = new Statement().GetResult(invoice, playDic);
            var expected = "Statement for BigCo\n Hamlet: $650.00 (55 seats)\n As You Like It: $580.00 (35 seats)\n Othello: $500.00 (40 seats)\nAmount owed is $1,730.00\nYou earned 47 credits!\n";
            Assert.AreEqual(expected, actual);
        }
    }
}
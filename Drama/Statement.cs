using System;
using System.Collections.Generic;
using System.Globalization;
using Drama.Models;

namespace Drama
{
    public class Statement
    {
        public string GetResult(Invoice invoice, Dictionary<string, Play> playDic)
        {
            var totalAmount = 0m;
            var volumeCredits = 0m;
            var result = $"Statement for { invoice.Customer}\n";

            foreach (var perf in invoice.Performances)
            {
                var play = playDic[perf.PlayID];
                var thisAmount = CalculateAmount(play, perf);

                volumeCredits += Math.Max(perf.Audience - 30, 0);
                if (play.Type == PlayType.Comedy)
                {
                    volumeCredits += Math.Floor(perf.Audience / 5m);
                }

                result += $" { play.Name}: { (thisAmount / 100).ToString("C2", new CultureInfo("en-US"))} ({ perf.Audience} seats)\n";
                totalAmount += thisAmount;
            }

            result += $"Amount owed is { (totalAmount / 100).ToString("C2", new CultureInfo("en-US"))}\n";
            result += $"You earned { volumeCredits} credits!\n";

            return result;
        }

        private static decimal CalculateAmount(Play play, Performance perf)
        {
            decimal result;
            switch (play.Type)
            {
                case PlayType.Tragedy:
                    result = 40000;
                    if (perf.Audience > 30)
                    {
                        result += 1000 * (perf.Audience - 30);
                    }

                    break;
                case PlayType.Comedy:
                    result = 30000;
                    if (perf.Audience > 20)
                    {
                        result += 10000 + 500 * (perf.Audience - 20);
                    }

                    result += 300 * perf.Audience;
                    break;
                default:
                    throw new Exception($"unknown type: {play.Type}");
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Drama.Models;

namespace Drama
{
    public class Statement
    {
        public string GetResult(Invoice invoice, Dictionary<string, Play> playDic)
        {
            var data = new StatementData();
            data.Customer = invoice.Customer;
            data.PerformanceDetails = invoice.Performances.Select(p =>
            {
                var detail = new PerformanceDetail();
                detail.Play     = playDic[p.PlayID];
                detail.PlayID   = p.PlayID;
                detail.Audience = p.Audience;

                return detail;
            });

            return RenderPlainText(invoice, playDic, data);
        }

        private static string RenderPlainText(Invoice invoice, Dictionary<string, Play> playDic, StatementData data)
        {
            var result = $"Statement for {data.Customer}\n";

            var volumeCredits = CalculateVolumeCredits(invoice, playDic);
            var totalAmount   = CalculateTotalAmount(data.PerformanceDetails);

            foreach (var detail in data.PerformanceDetails)
            {
                result +=
                    $" {detail.Play.Name}: {(CalculateAmount(detail) / 100).ToString("C2", new CultureInfo("en-US"))} ({detail.Audience} seats)\n";
            }

            result += $"Amount owed is {(totalAmount / 100).ToString("C2", new CultureInfo("en-US"))}\n";
            result += $"You earned {volumeCredits} credits!\n";

            return result;
        }

        private static decimal CalculateTotalAmount(IEnumerable<PerformanceDetail> details)
        {
            return details.Sum(CalculateAmount);
        }

        private static decimal CalculateVolumeCredits(Invoice invoice, Dictionary<string, Play> playDic)
        {
            var volumeCredits = 0m;
            foreach (var perf in invoice.Performances)
            {
                var play = playDic[perf.PlayID];
                volumeCredits += CalculateCredits(perf, play);
            }

            return volumeCredits;
        }

        private static decimal CalculateCredits(Performance perf, Play play)
        {
            var credits = Math.Max(perf.Audience - 30, 0m);
            if (play.Type == PlayType.Comedy)
            {
                credits += Math.Floor(perf.Audience / 5m);
            }

            return credits;
        }

        private static decimal CalculateAmount(PerformanceDetail detail)
        {
            decimal result;
            switch (detail.Play.Type)
            {
                case PlayType.Tragedy:
                    result = 40000;
                    if (detail.Audience > 30)
                    {
                        result += 1000 * (detail.Audience - 30);
                    }

                    break;
                case PlayType.Comedy:
                    result = 30000;
                    if (detail.Audience > 20)
                    {
                        result += 10000 + 500 * (detail.Audience - 20);
                    }

                    result += 300 * detail.Audience;
                    break;
                default:
                    throw new Exception($"unknown type: {detail.Play.Type}");
            }

            return result;
        }
    }
}
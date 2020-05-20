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
                detail.Amount   = CalculateAmount(detail);
                detail.Credits  = CalculateCredits(detail);

                return detail;
            }).ToList();
            data.TotalAmount   = data.PerformanceDetails.Sum(d => d.Amount);
            data.VolumeCredits = data.PerformanceDetails.Sum(d => d.Credits);

            return RenderPlainText(data);
        }

        private static string RenderPlainText(StatementData data)
        {
            var result = $"Statement for {data.Customer}\n";

            foreach (var detail in data.PerformanceDetails)
            {
                result +=
                    $" {detail.Play.Name}: {(detail.Amount / 100).ToString("C2", new CultureInfo("en-US"))} ({detail.Audience} seats)\n";
            }

            result += $"Amount owed is {(data.TotalAmount / 100).ToString("C2", new CultureInfo("en-US"))}\n";
            result += $"You earned {data.VolumeCredits} credits!\n";

            return result;
        }

        private static decimal CalculateCredits(PerformanceDetail detail)
        {
            var credits = Math.Max(detail.Audience - 30, 0m);
            if (detail.Play.Type == PlayType.Comedy)
            {
                credits += Math.Floor(detail.Audience / 5m);
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
using System;
using Drama.Models;

namespace Drama
{
    public class CalculatorFactory
    {
        public PerformanceCalculator Get(PlayType playType)
        {
            PerformanceCalculator result;
            switch (playType)
            {
                case PlayType.Tragedy:
                    result = new TragedyCalculator();

                    break;
                case PlayType.Comedy:
                    result = new ComedyCalculator();
                    break;
                default:
                    throw new Exception($"unknown type: {playType}");
            }

            return result;
        }
    }
}
using System;

namespace HHJobsCalculator.Core.Models.Excpetions
{
    public class CalculationExcpetion : Exception
    {
        public CalculationExcpetion(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

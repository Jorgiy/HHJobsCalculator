using System.Collections.Generic;

namespace HHJobsCalculator.Core.Models.Engine
{
    public class JobCalculationResult
    {
        public decimal Total { get; set; } = 0.00M;

        public List<CalculatedPrintItem> CalculatedPrintItems { get; set; } = new List<CalculatedPrintItem>();
    }
}

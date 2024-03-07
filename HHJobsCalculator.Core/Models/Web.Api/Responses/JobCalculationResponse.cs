using System.Collections.Generic;

namespace HHJobsCalculator.Core.Models.Web.Api.Responses
{
    public class JobCalculationResponse
    {
        /// <summary>
        /// Total cost of all items in a money format
        /// </summary>        
        public Money? Total { get; set; }

        /// <summary>
        /// Each print item cost with tax applied
        /// </summary>
        public List<CalculatedPrintItem>? CalculatedPrintItems { get; set; } = new List<CalculatedPrintItem>();
    }
}

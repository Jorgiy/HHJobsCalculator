using HHJobsCalculator.Core.Engine;
using HHJobsCalculator.Core.Models.Engine;
using HHJobsCalculator.Core.Models.Excpetions;
using HHJobsCalculator.Engine.Validators;
using System;

namespace HHJobsCalculator.Engine
{
    /// <summary>
    /// HH Job calculator
    /// </summary>
    public class JobsCalculator : IJobsCalculator
    {
        const decimal SalesTaxMultiplier = 0.07M;

        const decimal BaseAddedMarginMultiplier = 0.11M;

        const decimal ExtraAddedMarginMultiplier = 0.16M;

        // need for readability of the counting even cents "algorithm"
        const int CentsPerDollar = 100;

        /// <summary>
        /// Calculates total job cost with an items' costs after tax applied
        /// </summary>
        /// <param name="jobRequest"></param>
        /// <returns>Calculation result</returns>
        public JobCalculationResult CalculateJob(JobRequest jobRequest)
        {
            jobRequest.ValidateRequest();

            try
            {
                var result = new JobCalculationResult();
                decimal total = 0.0M;
                var marginMultiplier = jobRequest.ExtraMarginApplied ? ExtraAddedMarginMultiplier : BaseAddedMarginMultiplier;

                foreach (var printItem in jobRequest.PrintItems)
                {
                    var calculatedPrintItem = new CalculatedPrintItem { ItemName = printItem.ItemName };

                    // tax application
                    var cost = printItem.Value.Value + (printItem.TaxExtemptApplied ? 0 : printItem.Value.Value * SalesTaxMultiplier);
                    calculatedPrintItem.Cost = Math.Round(cost, 2, MidpointRounding.AwayFromZero);

                    result.CalculatedPrintItems.Add(calculatedPrintItem);

                    // margin application
                    var totalCents = (cost + printItem.Value.Value * marginMultiplier) * CentsPerDollar;
                    // rounding to even cents, MidpointRounding.ToEven has choosen only for "banking/financial concept" as a best option to middle point round decision in such type of context
                    total += Math.Round(totalCents/2, 0, MidpointRounding.ToEven) * 2 / CentsPerDollar; 
                }

                result.Total = total;
                return result;
            }
            catch (ArithmeticException arithmeticException)
            {                
                throw new CalculationExcpetion("Unable to parform calculations. Please contact system adimistrator", arithmeticException); // contact admin means 'go to app logs'
            }
        }
    }
}

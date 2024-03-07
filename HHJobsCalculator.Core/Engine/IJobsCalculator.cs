using HHJobsCalculator.Core.Models.Engine;

namespace HHJobsCalculator.Core.Engine
{
    /// <summary>
    /// HH Job calculator
    /// </summary>
    public interface IJobsCalculator
    {
        /// <summary>
        /// Calculates total job cost with an items' costs after tax applied
        /// </summary>
        /// <param name="jobRequest"></param>
        /// <returns>Calculation result</returns>
        JobCalculationResult CalculateJob(JobRequest jobRequest);
    }
}

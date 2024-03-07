using HHJobsCalculator.Core.Models.Engine;

namespace HHJobsCalculator.Core.Engine
{
    public interface IJobsCalculator
    {
        JobCalculationResult CalculateJob(JobRequest jobRequest);
    }
}

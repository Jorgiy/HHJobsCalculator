using HHJobsCalculator.Core.Models.Engine;
using Swashbuckle.AspNetCore.Filters;

namespace HHJobsCalculator.WebApi.SwaggerExamples
{
    public class JobRequestExample : IExamplesProvider<JobRequest>
    {
        public JobRequest GetExamples()
        {
            return new JobRequest
            {
                ExtraMarginApplied = true,
                PrintItems = new[] {
                    new PrintItem { ItemName = "envelopes", Value = 520.00M },
                    new PrintItem { ItemName = "letterhead", TaxExtemptApplied = true, Value = 1983.37M }
                }
            };
        }
    }
}

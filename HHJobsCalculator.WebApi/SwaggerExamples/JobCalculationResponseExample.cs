using HHJobsCalculator.Core.Models.Web.Api.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace HHJobsCalculator.WebApi.SwaggerExamples
{
    public class JobCalculationResponseExample : IExamplesProvider<JobCalculationResponse>
    {
        public JobCalculationResponse GetExamples()
        {
            return new JobCalculationResponse
            {
                CalculatedPrintItems = new List<CalculatedPrintItem>
                {
                    new CalculatedPrintItem
                    {
                        ItemName = "envelopes",
                        Cost = new Money(556.40M)
                    },
                    new CalculatedPrintItem
                    {
                        ItemName = "letterhead",
                        Cost = new Money(1983.37M)
                    }
                },
                Total = new Money(2940.30M)
            };
        }
    }
}

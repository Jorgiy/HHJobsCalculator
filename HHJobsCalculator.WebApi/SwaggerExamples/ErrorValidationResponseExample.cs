using HHJobsCalculator.Core.Models.Web.Api.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace HHJobsCalculator.WebApi.SwaggerExamples
{
    public class ErrorValidationResponseExample : IExamplesProvider<ErrorResponse>
    {
        public ErrorResponse GetExamples()
        {
            return new ErrorResponse
            {
                Error = "Item name could not be empty."
            };
        }
    }
}

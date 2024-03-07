using HHJobsCalculator.Core.Models.Web.Api.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace HHJobsCalculator.WebApi.SwaggerExamples
{
    public class ErrorResponseExample : IExamplesProvider<ErrorResponse>
    {
        public ErrorResponse GetExamples()
        {
            return new ErrorResponse
            {
                Error = "Unable to parform calculations. Please contact system adimistrator."
            };
        }
    }
}

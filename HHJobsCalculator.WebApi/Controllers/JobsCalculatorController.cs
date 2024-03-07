using HHJobsCalculator.Core.Engine;
using HHJobsCalculator.WebApi.SwaggerExamples;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;
using HHJobsCalculator.Core.Models.Web.Api.Responses;
using HHJobsCalculator.Core.Models.Engine;

namespace HHJobsCalculator.WebApi.Controllers
{
    [Route("api/v1/Jobs")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class JobsCalculatorController : ControllerBase
    {
        private readonly IJobsCalculator _jobsCalculator;

        public JobsCalculatorController(IJobsCalculator jobsCalculator)
        {
            _jobsCalculator = jobsCalculator;
        }

        /// <summary>
        /// Calculates total charge of a job to a customer 
        /// </summary>
        /// <param name="jobRequest">Job for calculation</param>
        /// <returns>Calculation result</returns>
        [HttpPost]        
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(JobCalculationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ErrorValidationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ErrorResponseExample))]
        [ProducesResponseType(typeof(JobCalculationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)] 
        [SwaggerRequestExample(typeof(JobRequest), typeof(JobRequestExample))]
        public JobCalculationResponse CalculateJob([FromBody] JobRequest jobRequest)
        {            
            var calculatedJobs = _jobsCalculator.CalculateJob(jobRequest);
            return MapJobResultToApiResponse(calculatedJobs);
        }

        private JobCalculationResponse MapJobResultToApiResponse(JobCalculationResult jobCalculationResult)
        {
            var response = new JobCalculationResponse();
                        
            response.Total = new Money(jobCalculationResult.Total); 
            foreach (var printItem in jobCalculationResult.CalculatedPrintItems)
            {
                response.CalculatedPrintItems.Add(new Core.Models.Web.Api.Responses.CalculatedPrintItem
                {
                    ItemName = printItem.ItemName,
                    Cost = new Money(printItem.Cost)
                });
            }
            return response;
        }
    }
}

using HHJobsCalculator.Core.Models.Excpetions;
using HHJobsCalculator.Core.Models.Validation;
using HHJobsCalculator.Core.Models.Web.Api.Responses;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace HHJobsCalculator.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // added request logging here to do not create multiple middlewares for request logging. Requests are not data-sensitive, full data logging allowed
                _logger.Info($"Request body: {await ReadRequestBody(context)}");
                await _next(context);
            }
            catch (ValidationException validationException)
            {
                _logger.Warn($"Validation error occured: {validationException.Message}");
                var response = context.Response;
                response.ContentType = MediaTypeNames.Application.Json;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                await response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse { Error = validationException.Message }));
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message, exception);
                var response = context.Response;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ContentType = MediaTypeNames.Application.Json;
                await response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse 
                { 
                    Error = exception is CalculationExcpetion ? exception.Message : "Unexpected error occured."
                }));             
            }
        }

        private async Task<string> ReadRequestBody(HttpContext context)
        {
            var body = string.Empty;
            context.Request.EnableBuffering();
            var requestStream = context.Request.Body;
            using (StreamReader reader = new StreamReader(requestStream, Encoding.UTF8, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
            }

            requestStream.Position = 0;
            return body;
        }
    }
}

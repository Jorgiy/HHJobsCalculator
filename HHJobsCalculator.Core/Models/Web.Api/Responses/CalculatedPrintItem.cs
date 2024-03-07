namespace HHJobsCalculator.Core.Models.Web.Api.Responses
{
    public class CalculatedPrintItem
    {
        /// <summary>
        /// Item name
        /// </summary>
        public string? ItemName { get; set; }

        /// <summary>
        /// Cost in a money format
        /// </summary>
        public Money? Cost { get; set; }
    }
}

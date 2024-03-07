namespace HHJobsCalculator.Core.Models.Engine
{
    public class PrintItem
    {
        /// <summary>
        /// Item name
        /// </summary>
        public string? ItemName { get; set; }

        /// <summary>
        /// Item value
        /// </summary>
        public decimal? Value { get; set; }

        /// <summary>
        /// Is an item a subject to tax exempt
        /// </summary>
        public bool TaxExtemptApplied { get; set; }
    }
}

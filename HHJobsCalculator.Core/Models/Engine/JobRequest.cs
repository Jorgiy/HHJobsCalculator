namespace HHJobsCalculator.Core.Models.Engine
{
    public class JobRequest
    {
        /// <summary>
        /// Apply extra margin
        /// </summary>
        public bool ExtraMarginApplied { get; set; }

        /// <summary>
        /// Set of items for calculation
        /// </summary>
        public PrintItem[]? PrintItems { get; set; }
    }
}

using HHJobsCalculator.Core.Models.Engine;
using HHJobsCalculator.Core.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HHJobsCalculator.Engine.Validators
{
    internal static class JobRequestValidator
    {
        public static void ValidateRequest(this JobRequest jobRequest)
        {
            if (jobRequest == null)
                throw new ValidationException("Jobs' request is empty.");

            if (jobRequest.PrintItems == null)
                throw new ValidationException("Print items are absent.");

            if (jobRequest.PrintItems.Length != 0)
            {
                var errors = new List<string>();

                foreach (var printItem in jobRequest.PrintItems)
                {
                    if (string.IsNullOrEmpty(printItem.ItemName))
                        throw new ValidationException("Item name could not be empty.");

                    if (printItem.Value == null)
                        errors.Add($"Item {printItem.ItemName} value is empty.");
                    else if (printItem.Value < 0)
                        errors.Add($"Item {printItem.ItemName} is negative.");                        
                }

                var itemNameGroups = jobRequest.PrintItems.GroupBy(item => item.ItemName);
                if (itemNameGroups.Count() < jobRequest.PrintItems.Length)
                    errors.Add($"Item names: {string.Join(", ", itemNameGroups.Where(group => group.Count() > 1).Select(group => group.Key))} are not unique.");

                if (errors.Count != 0)
                    throw new ValidationException(string.Join("; ", errors));               
            }
        }
    }
}

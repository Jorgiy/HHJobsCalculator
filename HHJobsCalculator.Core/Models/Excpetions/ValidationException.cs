using System;

namespace HHJobsCalculator.Core.Models.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string validationMessage) : base(validationMessage)
        {
        }
    }
}

using System;

namespace HHJobsCalculator.Core.Models.Web.Api.Responses
{
    public class Money
    {
        /// <summary>
        /// Creates a money instance rounds amount using banker's round with 2 decimal digits 
        /// </summary>
        /// <param name="amount"></param>
        public Money(decimal amount)
        {
            Amount = Math.Round(amount, MidpointRounding.ToEven); // need to do it just to type unification, actually engine controls 2 decimal digits
            DisplayAmount = amount.ToString("0.00");
        }

        /// <summary>
        /// Amount in numeric format
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// Currency code
        /// </summary>
        public string CurrencyCode { get; set; } = "USD"; // in this solution we use only dollars as currency

        /// <summary>
        /// Amount in human-readble currency format
        /// </summary>
        public string DisplayAmount { get; set; }
    }
}

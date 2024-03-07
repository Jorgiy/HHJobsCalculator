using HHJobsCalculator.Core.Models.Engine;
using HHJobsCalculator.Core.Models.Validation;
using HHJobsCalculator.Engine;
using NUnit.Framework;
using System;
using System.Linq;

namespace HHJobsCalculator.Tests
{
    public class Tests
    {
        private readonly JobsCalculator _jobsCalculator = new JobsCalculator();

        [Test]
        public void CalculateJob_ExtraMarginOnItemsWithAndWitoutExempt_CertainResultCalculated()
        {
            var result = _jobsCalculator.CalculateJob(new JobRequest
            {
                ExtraMarginApplied = true,
                PrintItems = new[] { 
                    new PrintItem { ItemName = "envelopes", Value = 520.00M }, 
                    new PrintItem { ItemName = "letterhead", TaxExtemptApplied = true, Value = 1983.37M } 
                }
            });

            Assert.Multiple(() =>
            {
                Assert.AreEqual(556.40M, result.CalculatedPrintItems.First(item => item.ItemName == "envelopes").Cost);
                Assert.AreEqual(1983.37M, result.CalculatedPrintItems.First(item => item.ItemName == "letterhead").Cost);
                Assert.AreEqual(2940.30M, result.Total);
            });
        }

        [Test]
        public void CalculateJob_BaseMarginOnItemWitoutExempt_CertainResultCalculated()
        {
            var result = _jobsCalculator.CalculateJob(new JobRequest
            {
                PrintItems = new[] {
                    new PrintItem { ItemName = "t-shirts", Value = 294.04M }
                }
            });

            Assert.Multiple(() =>
            {
                Assert.AreEqual(314.62M, result.CalculatedPrintItems.First(item => item.ItemName == "t-shirts").Cost);
                Assert.AreEqual(346.960M, result.Total);
            });
        }

        [Test]
        public void CalculateJob_ExtraMarginOnItemsWithExempt_CertainResultCalculated()
        {
            var result = _jobsCalculator.CalculateJob(new JobRequest
            {
                ExtraMarginApplied = true,
                PrintItems = new[] {
                    new PrintItem { ItemName = "frisbees", TaxExtemptApplied = true, Value = 19385.38M },
                    new PrintItem { ItemName = "yo-yos", TaxExtemptApplied = true, Value = 1829M }
                }
            });

            Assert.Multiple(() =>
            {
                Assert.AreEqual(19385.38M, result.CalculatedPrintItems.First(item => item.ItemName == "frisbees").Cost);
                Assert.AreEqual(1829.00, result.CalculatedPrintItems.First(item => item.ItemName == "yo-yos").Cost);
                Assert.AreEqual(24608.68M, result.Total);
            });
        }

        [Test]
        public void CalculateJob_EmptyRequest_ThrowsValidationException()
        {
            JobRequest jobRequest = null;

            Assert.Throws<ValidationException>(() => _jobsCalculator.CalculateJob(jobRequest));
        }

        [Test]
        public void CalculateJob_EmptyPrintItems_ThrowsValidationException()
        {
            var jobRequest = new JobRequest
            {
                ExtraMarginApplied = true
            };

            Assert.Throws<ValidationException>(() => _jobsCalculator.CalculateJob(jobRequest));
        }

        [Test]
        public void CalculateJob_EmptyItemNames_ThrowsValidationException()
        {
            var jobRequest = new JobRequest
            {
                ExtraMarginApplied = true,
                PrintItems = new[] {
                    new PrintItem { TaxExtemptApplied = true, Value = 19385.38M },
                    new PrintItem { Value = 1829M }
                }
            };

            Assert.Throws<ValidationException>(() => _jobsCalculator.CalculateJob(jobRequest));
        }

        [Test]
        public void CalculateJob_EmptyItemValues_ThrowsValidationException()
        {
            var jobRequest = new JobRequest
            {
                ExtraMarginApplied = true,
                PrintItems = new[] {
                    new PrintItem { ItemName = "frisbees", TaxExtemptApplied = true },
                    new PrintItem { ItemName = "yo-yos", Value = 1829M }
                }
            };

            Assert.Throws<ValidationException>(() => _jobsCalculator.CalculateJob(jobRequest));
        }

        [Test]
        public void CalculateJob_ArithmeticOverflow_ThrowsInnerArithmeticExcpetion()
        {
            var jobRequest = new JobRequest
            {
                ExtraMarginApplied = true,
                PrintItems = new[] {
                    new PrintItem { ItemName = "frisbees", TaxExtemptApplied = true, Value = decimal.MaxValue }
                }
            };

            try
            {
                _jobsCalculator.CalculateJob(jobRequest);
                Assert.Fail("No exception was thrown.");
            }
            catch (Exception exception)
            {
                Assert.IsNotNull(exception.InnerException);
                Assert.AreEqual(typeof(ArithmeticException), exception.InnerException.GetType().BaseType);
            }                  
        }
    }
}
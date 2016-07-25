using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RelayMotorInsuranceCalculator.DAL.Entities;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;
using RelayMotorInsuranceCalculator.Services;

namespace RelayMotorInsuranceCalculator.Tests
{
    [TestClass]
    public class PremiumCalculationServiceTests
    {
        private Policy _policy;
        public PremiumCalculationServiceTests()
        {
            _policy = new Policy
            {
                StartDate = DateTime.Today.AddDays(1),
                Drivers = new List<Driver>
                {
                    new Driver
                    {
                        FirstName = "Carlo",
                        LastName = "Hagens",
                        DateOfBirth = new DateTime(1991, 1, 12),
                        Occupation = Occupation.Accountant,
                        Claims = new List<Claim>()
                        {
                            new Claim
                            {
                                ClaimDate = new DateTime(2015, 7, 21)
                            },
                            new Claim
                            {
                                ClaimDate = new DateTime(2015, 7, 28)
                            }
                        }
                    },
                    new Driver
                    {
                        FirstName = "Nicky",
                        LastName = "Ernste",
                        DateOfBirth = new DateTime(1992, 2, 24),
                        Occupation =  Occupation.Chauffeur
                    },
                    new Driver
                    {
                        FirstName = "Roy",
                        LastName = "Cleven",
                        DateOfBirth = new DateTime(1994, 5, 23),
                        Occupation =  Occupation.Chauffeur
                    }
                }
            };
        }
        [TestMethod]
        public void DeclineCalculationTest()
        {
          
            var premiumCalculationService = new PremiumCalculationService();
            var policyDeclined = premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy);
            Assert.AreEqual(policyDeclined.PolicyDeclined, false);
        }

        [TestMethod]
        public void PremiumCalculationTest()
        {
           var premiumCalculationService = new PremiumCalculationService();
            var policyPremium = premiumCalculationService.CalculatePremium(_policy);
            Assert.AreEqual(policyPremium, 858);
        }
    }
}

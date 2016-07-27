using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RelayMotorInsuranceCalculator.DAL.Entities;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;
using RelayMotorInsuranceCalculator.Services;
using RelayMotorInsuranceCalculator.Services.Models.Enums;

namespace RelayMotorInsuranceCalculator.Tests
{
    [TestClass]
    public class PremiumCalculationServiceTests
    {
        private readonly Policy _policy1;
        private readonly Policy _policy2;
        private readonly Policy _policy3;
        private readonly Policy _policy4;
        private readonly Policy _policy5;
        private readonly Policy _policy6;
        private readonly Policy _policy7;


        public PremiumCalculationServiceTests()
        {
            _policy1 = new Policy
            {
                StartDate = DateTime.Today.AddDays(-1),
                Drivers = new List<Driver>
                {
                    new Driver
                    {
                        FirstName = "Carlo",
                        LastName = "Hagens",
                        DateOfBirth = new DateTime(1991, 1, 12),
                        Occupation = Occupation.Accountant
                    }
                }
            };
            _policy2 = new Policy
            {
                StartDate = DateTime.Today.AddDays(1),
                Drivers = new List<Driver>
                {
                    new Driver
                    {
                        FirstName = "Carlo",
                        LastName = "Hagens",
                        DateOfBirth = new DateTime(1998, 1, 12),
                        Occupation = Occupation.Accountant
                    }
                }
            };
            _policy3 = new Policy
            {
                StartDate = DateTime.Today.AddDays(1),
                Drivers = new List<Driver>
                {
                    new Driver
                    {
                        FirstName = "Carlo",
                        LastName = "Hagens",
                        DateOfBirth = new DateTime(1925, 1, 12),
                        Occupation = Occupation.Accountant
                    }
                }
            };
            _policy4 = new Policy
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
                                ClaimDate = new DateTime(2015, 8, 28)
                            }
                            ,
                            new Claim
                            {
                                ClaimDate = new DateTime(2014, 8, 28)
                            }
                        }
                    }
                }
            };
            _policy5 = new Policy
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
                                ClaimDate = new DateTime(2015, 8, 28)
                            }
                        }
                    },
                    new Driver
                    {
                        FirstName = "Nicky",
                        LastName = "Ernste",
                        DateOfBirth = new DateTime(1992, 2, 24),
                        Occupation =  Occupation.Chauffeur,
                        Claims = new List<Claim>
                        {
                            new Claim
                            {
                                ClaimDate = new DateTime(2015, 12, 12)
                            },
                            new Claim
                            {
                                ClaimDate = new DateTime(2015, 8, 28)
                            }
                        }
                    }
                }
            };
            _policy6 = new Policy
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
                                ClaimDate = DateTime.Today.AddYears(-1).AddMonths(-1)
                            },
                            new Claim
                            {
                                ClaimDate = DateTime.Today.AddYears(-1).AddMonths(1)
                            }
                        }
                    },
                    new Driver
                    {
                        FirstName = "Nicky",
                        LastName = "Ernste",
                        DateOfBirth = new DateTime(1992, 2, 24),
                        Occupation =  Occupation.Chauffeur,
                        Claims = new List<Claim>
                        {
                            new Claim
                            {
                                ClaimDate = DateTime.Today.AddYears(-1).AddMonths(6)
                            }
                        }
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
            _policy7 = new Policy
            {
                StartDate = DateTime.Today.AddDays(1),
                Drivers = new List<Driver>
                {
                    new Driver
                    {
                        FirstName = "Carlo",
                        LastName = "Hagens",
                        DateOfBirth = new DateTime(1985, 1, 12),
                        Occupation = Occupation.Accountant,
                        Claims = new List<Claim>()
                        {
                            new Claim
                            {
                                ClaimDate = DateTime.Today.AddYears(-1).AddMonths(-1)
                            },
                            new Claim
                            {
                                ClaimDate = DateTime.Today.AddYears(-1).AddMonths(1)
                            }
                        }
                    }
                }
            };
        }
        [TestMethod]
        public void DeclineCalculationTest()
        {
            var premiumCalculationService = new PremiumCalculationService();

            var policyDeclined = premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy1);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.StartDateBeforeCurrentDate);


            policyDeclined = premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy2);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.YoungestDriverTooYoung);

            policyDeclined = premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy3);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.OldestDriverTooOld);

            policyDeclined = premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy4);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.SingleDriverMoreThanTwoClaims);

            policyDeclined = premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy5);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.TotalMoreThanThreeClaims);

            policyDeclined = premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy6);
            Assert.AreEqual(policyDeclined.PolicyDeclined, false);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, null);
        }

        [TestMethod]
        public void PremiumCalculationTest()
        {
            var premiumCalculationService = new PremiumCalculationService();

            var policyPremium = premiumCalculationService.CalculatePremium(_policy1);
            Assert.AreEqual(policyPremium, -1);

            policyPremium = premiumCalculationService.CalculatePremium(_policy2);
            Assert.AreEqual(policyPremium, -1);

            policyPremium = premiumCalculationService.CalculatePremium(_policy3);
            Assert.AreEqual(policyPremium, -1);

            policyPremium = premiumCalculationService.CalculatePremium(_policy4);
            Assert.AreEqual(policyPremium, -1);

            policyPremium = premiumCalculationService.CalculatePremium(_policy5);
            Assert.AreEqual(policyPremium, -1);

            policyPremium = premiumCalculationService.CalculatePremium(_policy6);
            Assert.AreEqual(policyPremium, 990);

            policyPremium = premiumCalculationService.CalculatePremium(_policy7);
            Assert.AreEqual(policyPremium, 526.5m);
        }
    }
}

﻿using System;
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
        private readonly IPremiumCalculationService _premiumCalculationService;



        /// <summary>
        /// Initializes a couple of policies, each with different details.
        /// </summary>
        public PremiumCalculationServiceTests()
        {
            _premiumCalculationService = new PremiumCalculationService();
            _policy1 = new Policy
            {
                StartDate = DateTime.Today.AddDays(-1),
                Drivers = new List<Driver>
                {
                    new Driver
                    {
                        Name = "Carlo",
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
                        Name = "Carlo",
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
                        Name = "Carlo",
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
                        Name = "Carlo",
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
                        Name = "Carlo",
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
                        Name = "Nicky",
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
                        Name = "Carlo",
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
                        Name = "Nicky",
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
                        Name = "Roy",
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
                        Name = "Carlo",
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

        /// <summary>
        /// Tests whether the policy is declined and if it is declined for the correct reason.
        /// </summary>
        [TestMethod]
        public void DeclineDeterminationTest()
        {
            var policyDeclined = _premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy1);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.StartDateBeforeCurrentDate);


            policyDeclined = _premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy2);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.YoungestDriverTooYoung);

            policyDeclined = _premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy3);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.OldestDriverTooOld);

            policyDeclined = _premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy4);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.SingleDriverMoreThanTwoClaims);

            policyDeclined = _premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy5);
            Assert.AreEqual(policyDeclined.PolicyDeclined, true);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, PolicyDeclineReason.TotalMoreThanThreeClaims);

            policyDeclined = _premiumCalculationService.DetermineIfPolicyShouldBeDeclined(_policy6);
            Assert.AreEqual(policyDeclined.PolicyDeclined, false);
            Assert.AreEqual(policyDeclined.PolicyDeclineReason, null);
        }

        /// <summary>
        /// Tests whether the premium calculation returns the correct values for each policy.
        /// </summary>
        [TestMethod]
        public void PremiumCalculationTest()
        {
            var policyPremium = _premiumCalculationService.CalculatePremium(_policy5);
            Assert.AreEqual(policyPremium, -1);

            policyPremium = _premiumCalculationService.CalculatePremium(_policy6);
            Assert.AreEqual(policyPremium, 990);

            policyPremium = _premiumCalculationService.CalculatePremium(_policy7);
            Assert.AreEqual(policyPremium, 526.5m);
        }
    }
}

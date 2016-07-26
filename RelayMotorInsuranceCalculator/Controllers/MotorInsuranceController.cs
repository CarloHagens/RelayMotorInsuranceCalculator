using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RelayMotorInsuranceCalculator.DAL.Entities;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;
using RelayMotorInsuranceCalculator.Services;
using RelayMotorInsuranceCalculator.ViewModels;

namespace RelayMotorInsuranceCalculator.Controllers
{
    public class MotorInsuranceController : BaseController
    {
        private readonly IPremiumCalculationService _premiumCalculationService;

        public MotorInsuranceController(IPremiumCalculationService premiumCalculationService)
        {
            _premiumCalculationService = premiumCalculationService;
        }
        public ActionResult PremiumCalculator(PremiumCalculatorVm premiumCalculatorVm)
        {
            premiumCalculatorVm = new PremiumCalculatorVm
            {
                Policy = new PolicyVm
                {
                    StartDate = DateTime.Today
                }
            };
            var policy = new Policy
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
                        Occupation =  Occupation.Chauffeur,
                        Claims = new List<Claim>
                        {
                            new Claim
                            {
                                ClaimDate = new DateTime(2015, 12, 12)
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
            _premiumCalculationService.CalculatePremium(policy);
            return View(premiumCalculatorVm);
        }
    }
}
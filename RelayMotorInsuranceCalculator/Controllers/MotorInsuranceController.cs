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
            return View(premiumCalculatorVm);
        }
    }
}
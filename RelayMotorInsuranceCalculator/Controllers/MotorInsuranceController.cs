using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RelayMotorInsuranceCalculator.DAL.Entities;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;
using RelayMotorInsuranceCalculator.Extensions;
using RelayMotorInsuranceCalculator.Services;
using RelayMotorInsuranceCalculator.Services.Models.Enums;
using RelayMotorInsuranceCalculator.ViewModels;
using RelayMotorInsuranceCalculator.ViewModels.MotorInsurance;

namespace RelayMotorInsuranceCalculator.Controllers
{
    public class MotorInsuranceController : BaseController
    {
        private readonly IPremiumCalculationService _premiumCalculationService;
        private readonly IMapper _mapper;

        public MotorInsuranceController(IPremiumCalculationService premiumCalculationService, IMapper mapper)
        {
            _premiumCalculationService = premiumCalculationService;
            _mapper = mapper;
        }
        public ActionResult PremiumCalculator()
        {
            var vm = new PremiumCalculatorVm
            {
                Policy = new PolicyVm
                {
                    StartDate = DateTime.Today,
                    Drivers = new List<DriverVm>
                    {
                        new DriverVm
                        {
                            DateOfBirth = DateTime.Today
                        }
                    }
                }
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddDriver(PremiumCalculatorVm vm)
        {
            vm.Policy.Drivers.Add(new DriverVm
            {
                DateOfBirth = DateTime.Today
            });
            return PartialView("_Drivers", vm);
        }

        [HttpPost]
        public ActionResult RemoveDriver(PremiumCalculatorVm vm, int index)
        {
            var driverToRemove = vm.Policy.Drivers[index];
            vm.Policy.Drivers.Remove(driverToRemove);
            return PartialView("_Drivers", vm);
        }

        [HttpPost]
        public ActionResult AddClaim(PremiumCalculatorVm vm, int index)
        {
            var driver = vm.Policy.Drivers[index];
            if (driver.Claims == null)
            {
                driver.Claims = new List<ClaimVm>
                {
                    new ClaimVm
                    {
                        ClaimDate = DateTime.Today
                    }
                };
            }
            else
            {
                driver.Claims.Add(new ClaimVm
                {
                    ClaimDate = DateTime.Today
                });
            }
            return PartialView("_Drivers", vm);
        }

        [HttpPost]
        public ActionResult RemoveClaim(PremiumCalculatorVm vm, int driver, int claim)
        {
            var claimToRemove = vm.Policy.Drivers[driver].Claims[claim];
            vm.Policy.Drivers[driver].Claims.Remove(claimToRemove);
            return PartialView("_Drivers", vm);
        }

        [HttpPost]
        public JsonResult Calculate(PremiumCalculatorVm vm)
        {
            var policy = _mapper.Map<Policy>(vm.Policy);
            var declined = _premiumCalculationService.DetermineIfPolicyShouldBeDeclined(policy);
            if (!declined.PolicyDeclined)
            {
                var premium = _premiumCalculationService.CalculatePremium(policy);
                return Json(new { message = "Your policy premium would be: £" + decimal.Round(premium, 2), messageType = "success" });
            }
            return Json(new { message = "Unfortunately your policy has been declined with the reason: "+ declined.PolicyDeclineReason.GetDescription(), messageType = "danger" });
        }
    }
}
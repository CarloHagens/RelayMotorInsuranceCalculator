using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RelayMotorInsuranceCalculator.DAL.Entities;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;
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
                        new DriverVm()
                    }
                }
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddDriver(PremiumCalculatorVm vm)
        {
            vm.Policy.Drivers.Add(new DriverVm());
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
                return Json(new { message = premium });
            }
            return Json(new { message = Enum.GetName(typeof(PolicyDeclineReason), declined.PolicyDeclineReason) });
        }
    }
}
using System;
using System.Collections.Generic;

namespace RelayMotorInsuranceCalculator.ViewModels.MotorInsurance
{
    public class PolicyVm
    {
        public List<DriverVm> Drivers { get; set; }
        public DateTime StartDate { get; set; }
    }
}
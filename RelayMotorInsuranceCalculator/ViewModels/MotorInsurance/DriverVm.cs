using System;
using System.Collections.Generic;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;

namespace RelayMotorInsuranceCalculator.ViewModels.MotorInsurance
{
    public class DriverVm
    {
        public List<ClaimVm> Claims { get; set; }
        public PolicyVm Policy { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => FirstName + ' ' + LastName;

        public Occupation Occupation { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
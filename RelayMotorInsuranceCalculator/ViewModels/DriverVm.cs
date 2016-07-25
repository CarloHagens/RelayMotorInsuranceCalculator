using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;

namespace RelayMotorInsuranceCalculator.ViewModels
{
    public class DriverVm
    {
        public List<ClaimVm> Claims { get; set; }
        public PolicyVm Policy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Occupation Occupation { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
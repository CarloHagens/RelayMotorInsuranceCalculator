using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;

namespace RelayMotorInsuranceCalculator.ViewModels.MotorInsurance
{
    public class DriverVm
    {
        public List<ClaimVm> Claims { get; set; }
        public PolicyVm Policy { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required]
        public Occupation Occupation { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
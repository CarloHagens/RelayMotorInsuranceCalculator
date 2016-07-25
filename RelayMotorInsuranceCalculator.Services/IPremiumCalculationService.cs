using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelayMotorInsuranceCalculator.DAL.Entities;
using RelayMotorInsuranceCalculator.Services.Models.Enums;

namespace RelayMotorInsuranceCalculator.Services
{
    public interface IPremiumCalculationService
    {
        decimal CalculatePremium(Policy policy);
        PolicyDecline DetermineIfPolicyShouldBeDeclined(Policy policy);
    }
}

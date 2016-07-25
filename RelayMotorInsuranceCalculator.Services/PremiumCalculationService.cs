using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RelayMotorInsuranceCalculator.DAL.Entities;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;
using RelayMotorInsuranceCalculator.Services.Helpers;
using RelayMotorInsuranceCalculator.Services.Models.Enums;

namespace RelayMotorInsuranceCalculator.Services
{
    public class PremiumCalculationService : IPremiumCalculationService
    {
        private const decimal Basepremium = 500;
        private const decimal BaseMultiplier = 1.00m;
        private const decimal OccupationDecreaseMultiplier = 0.10m;
        private const decimal OccupationIncreaseMultiplier = 0.10m;
        private const decimal AgeDecreaseMultiplier = 0.10m;
        private const decimal AgeIncreaseMultiplier = 0.20m;
        private const decimal ClaimLessThanOneYearMultiplier = 0.20m;
        private const decimal ClaimLessThanFiveYearsMultiplier = 0.10m;



        /// <summary>
        /// Calculates the premium that belongs to the passed policy.
        /// </summary>
        /// <param name="policy">The policy for which the premium should be calculated.</param>
        /// <returns>The calculated policy premium.</returns>
        public decimal CalculatePremium(Policy policy)
        {
            var currentPremium = Basepremium;
            OccupationCalculation(ref currentPremium, policy.Drivers);
            var youngestDriver = policy.Drivers.OrderByDescending(o => o.DateOfBirth).First();
            AgeCalculation(ref currentPremium, youngestDriver.DateOfBirth, policy.StartDate);
            var allClaims = new List<Claim>();
            foreach (var driver in policy.Drivers)
            {
                if (driver.Claims != null)
                {
                    allClaims.AddRange(driver.Claims);
                }
            }
            if (allClaims.Count > 0)
            {
                ClaimCalculation(ref currentPremium, allClaims, policy.StartDate);
            }
            return currentPremium;
        }
        private void OccupationCalculation(ref decimal currentPremium, ICollection<Driver> drivers)
        {
            var multiplier = BaseMultiplier;
            foreach (Driver driver in drivers)
            {
                switch (driver.Occupation)
                {
                    case Occupation.Accountant:
                        multiplier -= OccupationDecreaseMultiplier;
                        break;
                    case Occupation.Chauffeur:
                        multiplier += OccupationIncreaseMultiplier;
                        break;
                }
            }
            currentPremium *= multiplier;
        }

        private void AgeCalculation(ref decimal currentPremium, DateTime youngestDriverDateOfBirth, DateTime policyStartDate)
        {
            var age = AgeCalculationHelper.DetermineAgeOnDate(youngestDriverDateOfBirth, policyStartDate);
            var multiplier = BaseMultiplier;
            if (age >= 21 && age <= 25)
            {
                multiplier += AgeIncreaseMultiplier;
            }
            else if (age >= 26 && age <= 75)
            {

                multiplier -= AgeDecreaseMultiplier;
            }
            currentPremium *= multiplier;
        }


        private void ClaimCalculation(ref decimal currentPremium, ICollection<Claim> claims, DateTime policyStartDate)
        {
            var multiplier = BaseMultiplier;
            foreach (Claim claim in claims)
            {
                var claimAge = AgeCalculationHelper.DetermineAgeOnDate(claim.ClaimDate, policyStartDate);
                if (claimAge < 1)
                {
                    multiplier += ClaimLessThanOneYearMultiplier;
                }
                else if (claimAge < 5)
                {
                    multiplier += ClaimLessThanFiveYearsMultiplier;
                }
                else
                {
                    //Do nothing.
                }
            }
            currentPremium *= multiplier;
        }

        public PolicyDecline DetermineIfPolicyShouldBeDeclined(Policy policy)
        {
            if (policy.StartDate < DateTime.Today)
            {
                return new PolicyDecline
                {
                    PolicyDeclined = true,
                    PolicyDeclineReason = PolicyDeclineReason.StartDate
                };
            }
            var youngestDriver = policy.Drivers.OrderByDescending(o => o.DateOfBirth).First();
            if (AgeCalculationHelper.DetermineAgeOnDate(youngestDriver.DateOfBirth, policy.StartDate) < 21)
            {
                return new PolicyDecline
                {
                    PolicyDeclined = true,
                    PolicyDeclineReason = PolicyDeclineReason.YoungestDriver
                };
            }
            var oldestDriver = policy.Drivers.OrderBy(o => o.DateOfBirth).First();
            if (AgeCalculationHelper.DetermineAgeOnDate(oldestDriver.DateOfBirth, policy.StartDate) > 75)
            {
                return new PolicyDecline
                {
                    PolicyDeclined = true,
                    PolicyDeclineReason = PolicyDeclineReason.OldestDriver
                };
            }
            var totalClaims = 0;
            foreach (var driver in policy.Drivers)
            {
                if (driver.Claims != null)
                {
                    if (driver.Claims.Count > 2)
                    {
                        return new PolicyDecline
                        {
                            PolicyDeclined = true,
                            PolicyDeclineReason = PolicyDeclineReason.TwoClaims
                        };
                    }
                    totalClaims += driver.Claims.Count;
                    if (totalClaims > 3)
                    {
                        return new PolicyDecline
                        {
                            PolicyDeclined = true,
                            PolicyDeclineReason = PolicyDeclineReason.ThreeClaims
                        };
                    }
                }
            }
            return new PolicyDecline
            {
                PolicyDeclined = false
            };
        }
    }
}
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
    /// <summary>
    /// Service that is responsible for calculating premiums of policies as well as determining if a policy should be accepted or declined.
    /// </summary>
    public class PremiumCalculationService : IPremiumCalculationService
    {
        /// <summary>
        /// The base premium for a policy.
        /// </summary>
        private const decimal Basepremium = 500;
        /// <summary>
        /// The base multiplier for calculating changes to the premium.
        /// </summary>
        private const decimal BaseMultiplier = 1.00m;
        /// <summary>
        /// The value to substract from the base multiplier if the occupation is accountant.
        /// </summary>
        private const decimal OccupationDecreaseMultiplier = 0.10m;
        /// <summary>
        /// The value to add to the base multiplier if the occupation is chauffeur.
        /// </summary>
        private const decimal OccupationIncreaseMultiplier = 0.10m;
        /// <summary>
        /// The value to subtract from the base multiplier if the age is between 26 and 75.
        /// </summary>
        private const decimal AgeDecreaseMultiplier = 0.10m;
        /// <summary>
        /// The value to add to the base multiplier if the age is between 21 and 25.
        /// </summary>
        private const decimal AgeIncreaseMultiplier = 0.20m;
        /// <summary>
        /// The value to add to the base multiplier if a driver has a claim that is less than one year old.
        /// </summary>
        private const decimal ClaimLessThanOneYearMultiplier = 0.20m;
        /// <summary>
        /// The value to add to the base multiplier if a driver has a claim that is more than 1 and less than 5 years old.
        /// </summary>
        private const decimal ClaimLessThanFiveYearsMultiplier = 0.10m;



        /// <summary>
        /// Calculates the premium that belongs to the passed policy.
        /// </summary>
        /// <param name="policy">The policy for which the premium should be calculated.</param>
        /// <returns>The calculated policy premium. Returns -1 if policy is declined.</returns>
        public decimal CalculatePremium(Policy policy)
        {
            if (DetermineIfPolicyShouldBeDeclined(policy).PolicyDeclined)
            {
                return -1;
            }
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

        /// <summary>
        /// Calculates the new premium based on driver occupation.
        /// </summary>
        /// <param name="currentPremium">The premium that the policy is currently at.</param>
        /// <param name="drivers">A collection of all drivers on the policy.</param>
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

        /// <summary>
        /// Calculates the new premium based on driver age.
        /// </summary>
        /// <param name="currentPremium">The premium that the policy is currently at.</param>
        /// <param name="youngestDriverDateOfBirth">Date of birth of the youngest driver.</param>
        /// <param name="policyStartDate">The date on which the policy starts.</param>
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

        /// <summary>
        /// Calculates the new premium based on driver claims.
        /// </summary>
        /// <param name="currentPremium">The premium that the policy is currently at.</param>
        /// <param name="claims">A collection of all claims on the policy.</param>
        /// <param name="policyStartDate">The start date of the policy.</param>
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
            }
            currentPremium *= multiplier;
        }

        /// <summary>
        /// Determines if a policy should be declined.
        /// </summary>
        /// <param name="policy">The policy which should be accepted or declined.</param>
        /// <returns>A policy decline object containing whether it was declined or not including the reason.</returns>
        public PolicyDecline DetermineIfPolicyShouldBeDeclined(Policy policy)
        {
            if (policy.StartDate < DateTime.Today)
            {
                return new PolicyDecline
                {
                    PolicyDeclined = true,
                    PolicyDeclineReason = PolicyDeclineReason.StartDateBeforeCurrentDate
                };
            }
            var youngestDriver = policy.Drivers.OrderByDescending(o => o.DateOfBirth).First();
            if (AgeCalculationHelper.DetermineAgeOnDate(youngestDriver.DateOfBirth, policy.StartDate) < 21)
            {
                return new PolicyDecline
                {
                    PolicyDeclined = true,
                    PolicyDeclineReason = PolicyDeclineReason.YoungestDriverTooYoung
                };
            }
            var oldestDriver = policy.Drivers.OrderBy(o => o.DateOfBirth).First();
            if (AgeCalculationHelper.DetermineAgeOnDate(oldestDriver.DateOfBirth, policy.StartDate) > 75)
            {
                return new PolicyDecline
                {
                    PolicyDeclined = true,
                    PolicyDeclineReason = PolicyDeclineReason.OldestDriverTooOld
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
                            PolicyDeclineReason = PolicyDeclineReason.SingleDriverMoreThanTwoClaims
                        };
                    }
                    totalClaims += driver.Claims.Count;
                    if (totalClaims > 3)
                    {
                        return new PolicyDecline
                        {
                            PolicyDeclined = true,
                            PolicyDeclineReason = PolicyDeclineReason.TotalMoreThanThreeClaims
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
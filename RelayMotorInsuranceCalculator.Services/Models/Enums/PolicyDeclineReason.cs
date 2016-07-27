using System.ComponentModel;

namespace RelayMotorInsuranceCalculator.Services.Models.Enums
{
    /// <summary>
    /// The reason for declining a policy.
    /// </summary>
    public enum PolicyDeclineReason
    {
        [Description("The policy start date is before the current date.")]
        StartDateBeforeCurrentDate,
        [Description("The youngest driver on the policy is below the age of 21.")]
        YoungestDriverTooYoung,
        [Description("The oldest driver on the policy is above the age of 75.")]
        OldestDriverTooOld,
        [Description("One of the drivers has 3 or more claims.")]
        SingleDriverMoreThanTwoClaims,
        [Description("Combined the drivers have 4 or more claims.")]
        TotalMoreThanThreeClaims
    }
}
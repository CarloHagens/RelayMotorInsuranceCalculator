using System.ComponentModel;

namespace RelayMotorInsuranceCalculator.Services.Models.Enums
{
    /// <summary>
    /// The reason for declining a policy.
    /// </summary>
    public enum PolicyDeclineReason
    {
        [Description("Start Date of Policy")]
        StartDateBeforeCurrentDate,
        [Description("Age of Youngest Driver")]
        YoungestDriverTooYoung,
        [Description("Age of Oldest Driver")]
        OldestDriverTooOld,
        [Description("Driver has more than 2 claims")]
        SingleDriverMoreThanTwoClaims,
        [Description("Policy has more than 3 claims")]
        TotalMoreThanThreeClaims
    }
}
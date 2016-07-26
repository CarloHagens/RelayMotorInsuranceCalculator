namespace RelayMotorInsuranceCalculator.Services.Models.Enums
{
    /// <summary>
    /// The reason for declining a policy.
    /// </summary>
    public enum PolicyDeclineReason
    {
        StartDateBeforeCurrentDate, YoungestDriverTooYoung, OldestDriverTooOld, SingleDriverMoreThanTwoClaims, TotalMoreThanThreeClaims
    }
}
namespace RelayMotorInsuranceCalculator.Services.Models.Enums
{
    /// <summary>
    /// Contains whether a policy was declined or not including the reason for it.
    /// </summary>
    public class PolicyDecline
    {
        /// <summary>
        /// True if policy declined, false if accepted.
        /// </summary>
        public bool PolicyDeclined { get; set; }
        /// <summary>
        /// The reason for declining a policy.
        /// </summary>
        public PolicyDeclineReason? PolicyDeclineReason { get; set; }
    }
}
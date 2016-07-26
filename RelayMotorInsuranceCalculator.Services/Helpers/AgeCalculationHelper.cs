using System;

namespace RelayMotorInsuranceCalculator.Services.Helpers
{
    /// <summary>
    /// Helper class to calculate the age of something in full years.
    /// </summary>
    public class AgeCalculationHelper
    {
        /// <summary>
        /// Calculates the age of something on a certain date.
        /// </summary>
        /// <param name="occuranceDate">The date on which the event occurred.</param>
        /// <param name="requestDate">The date at which the age of the event that occurred should be calculated.</param>
        /// <returns></returns>
        public static int DetermineAgeOnDate(DateTime occuranceDate, DateTime requestDate)
        {
            var age = requestDate.Year - occuranceDate.Year;
            if (requestDate < occuranceDate.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
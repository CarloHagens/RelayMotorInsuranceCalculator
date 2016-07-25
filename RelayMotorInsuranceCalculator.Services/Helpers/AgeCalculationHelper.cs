using System;

namespace RelayMotorInsuranceCalculator.Services.Helpers
{
    public class AgeCalculationHelper
    {
        public static int DetermineAgeOnDate(DateTime dateOfBirth, DateTime requestDate)
        {
            var age = requestDate.Year - dateOfBirth.Year;
            if (requestDate < dateOfBirth.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
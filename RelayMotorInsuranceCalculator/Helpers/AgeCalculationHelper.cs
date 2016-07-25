using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelayMotorInsuranceCalculator.Helpers
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
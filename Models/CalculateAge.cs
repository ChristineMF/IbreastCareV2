using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IbreastCare.Models
{
    public class CalculateAge
    {
        public int CalculateAgeCorrect(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }
    }
}
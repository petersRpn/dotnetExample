using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Services.Helpers
{
    public class MaximumAgeValidatorAttribute : ValidationAttribute
    {
        public int MaximumYear { get; set; } = 2001;
        public string DefaultErrorMessage { get; set; } = "Year should not be more than {0}";

        //parameterized constructor
        public MaximumAgeValidatorAttribute(int maximumYear)
        {
            MaximumYear = maximumYear;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Year <= MaximumYear)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MaximumYear));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}

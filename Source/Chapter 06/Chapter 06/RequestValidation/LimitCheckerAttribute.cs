using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RequestValidation
{
    public class LimitCheckerAttribute : ValidationAttribute
    {
        public LimitCheckerAttribute(string baseProperty, double percentage)
        {
            this.BaseProperty = baseProperty;
            this.Percentage = percentage;
            this.ErrorMessage = "{0} cannot exceed {1}% of {2}";
        }

        public string BaseProperty { get; set; }
        public double Percentage { get; set; }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, this.Percentage, BaseProperty);
        }

        protected override ValidationResult IsValid(object value,
                                                        ValidationContext validationContext)
        {
            decimal amount = (decimal)value;

            var propertyInfo = validationContext
                                            .ObjectType
                                               .GetProperty(this.BaseProperty);

            if (propertyInfo != null)
            {
                decimal baseAmount = (decimal)propertyInfo.GetValue(
                                                                           validationContext.ObjectInstance, null);

                decimal maxLimit = baseAmount * (decimal)this.Percentage / 100;

                if (amount <= maxLimit)
                    return ValidationResult.Success;
            }

            return new ValidationResult(
                        FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
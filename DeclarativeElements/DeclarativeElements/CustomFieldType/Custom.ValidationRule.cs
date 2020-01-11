using System.Windows.Controls;
using System.Globalization;
using System;

namespace CustomField.System.Windows.Controls
{
    public class CustomValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String inputedValue = (String)value;
            if (inputedValue == "notAllowed")
                return new ValidationResult(false, "Value Is Not Valid");

            return new ValidationResult(true, "This Is Valid");
        }
    }
}

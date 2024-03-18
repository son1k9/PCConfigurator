using System.Globalization;
using System.Windows.Controls;

namespace PCConfigurator.Validation
{
    internal class NotNullRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "This property can not be null.");
            else
                return ValidationResult.ValidResult;
        }
    }
}

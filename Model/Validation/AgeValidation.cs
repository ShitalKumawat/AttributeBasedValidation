using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Model.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class AgeValidation: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var val = Convert.ToInt32(value);
            return (val != 99);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}

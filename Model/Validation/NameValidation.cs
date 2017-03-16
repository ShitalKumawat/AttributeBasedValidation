using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Model.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NameValidation: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var val = Convert.ToString(value);
            return (val != "XXX");
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}

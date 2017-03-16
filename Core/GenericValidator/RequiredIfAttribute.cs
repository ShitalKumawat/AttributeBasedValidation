using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GenericValidator
{
    public sealed class RequiredIfAttribute : ValidationAttribute
    {
        public string PropertyName { get; private set; }
        public object[] Values { get; private set; }

        public RequiredIfAttribute(string propertyName, params object[] equalsValues)
        {
            this.PropertyName = propertyName;
            this.Values = equalsValues;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isRequired = this.IsRequired(validationContext);

            if (isRequired && string.IsNullOrEmpty(Convert.ToString(value)))
                return new ValidationResult(this.ErrorMessage);
            return ValidationResult.Success;
        }

        private bool IsRequired(ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(this.PropertyName);
            var currentValue = property.GetValue(validationContext.ObjectInstance, null);

            foreach (var val in this.Values)
                if (object.Equals(currentValue, val))
                    return true;
            return false;
        }
    }
}

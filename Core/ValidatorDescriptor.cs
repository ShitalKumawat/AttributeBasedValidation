using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Core
{
    public class ValidatorDescriptor
    {
        public PropertyInfo Property { get; private set; }
        public ValidationAttribute ValidationAttribute { get; private set; }

        public ValidatorDescriptor(PropertyInfo property, ValidationAttribute validationAttribute)
        {
            this.Property = property;
            this.ValidationAttribute = validationAttribute;
        }

        public object GetValue(object instance)
        {
            return this.Property.GetValue(instance, null);
        }
    }
}

using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Core
{
    public abstract class BaseClassValidation : BindableBase, IDataErrorInfo
    {
        private static Dictionary<Type, ValidatorDescriptor[]> ValidatorDescriptorCache { get; set; }

        private ValidationContext ValidationContext { get; set; }


        private static IEnumerable<ValidatorDescriptor> GetValidatorDescriptors(Type type)
        {
            if (ValidatorDescriptorCache.ContainsKey(type))
                return ValidatorDescriptorCache[type];

            var list = new List<ValidatorDescriptor>();
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var validationAttributes = (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
                list.AddRange(validationAttributes.Select(attr => new ValidatorDescriptor(property, attr)));
            }
            var array = list.ToArray();
            ValidatorDescriptorCache.Add(type, array);
            return array;
        }

        private static string[] Validate(ValidationContext validationContext, string propertyName)
        {
            if (validationContext == null || validationContext.ObjectInstance == null)
                throw new ArgumentNullException("validationContext", "Validation context must be provided and the ObjectInstance must be set.");

            return GetValidatorDescriptors(validationContext.ObjectType)
                .Where(v => v.Property.Name == propertyName)
                .Where(v => v.ValidationAttribute.GetValidationResult(v.GetValue(validationContext.ObjectInstance), validationContext) != ValidationResult.Success)
                .Select(v => v.ValidationAttribute.ErrorMessage).ToArray();
        }

        protected BaseClassValidation()
        {
            ValidatorDescriptorCache = new Dictionary<Type, ValidatorDescriptor[]>();
            this.ValidationContext = new ValidationContext(this, null, null);
        }


        public string this[string columnName]
        {
            get
            {
                var errors = Validate(this.ValidationContext, columnName).ToList();
                return (errors.Count == 0 ? string.Empty : string.Join(Environment.NewLine, errors));
            }
        }

        public string Error
        {
            get
            {
                var validators = GetValidatorDescriptors(this.GetType()).ToList();
                if (!validators.Any()) return string.Empty;

                var errors = new List<string>();
                foreach (var result in validators.Select(v => Validate(this.ValidationContext, v.Property.Name)
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToList()).Where(result => result.Any()))
                {
                    errors.AddRange(result);
                }
                return (errors.Count == 0 ? string.Empty : string.Join(Environment.NewLine, errors));
            }
        }
    }
}

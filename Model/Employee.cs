using Core;
using Model.Validation;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Employee : BaseClassValidation
    {
        [Required(ErrorMessage = "Mandatory Field.")]
        [NameValidation(ErrorMessage = "Name is not valid.")]
        [MaxLength(2,ErrorMessage ="Maximum Length is 10")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mandatory Field.")]
        [AgeValidation(ErrorMessage = "Age is not valid.")]
        public int Age { get; set; }
    }
}

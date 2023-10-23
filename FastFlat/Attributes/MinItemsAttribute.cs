using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FastFlat.Attributes
{
    public class MinItemsAttribute : ValidationAttribute
    {
        private readonly int _minItems;

        public MinItemsAttribute(int minItems)
        {
            _minItems = minItems;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IList list && list.Count >= _minItems)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? $"Du må velge minst {_minItems} element(er).");
        }
    }

}

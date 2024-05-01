
using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.DTO.CouponDTOs
{
    internal class DateRangeAttribute : ValidationAttribute
    {
        private readonly string _fromDatePropertyName;

        public DateRangeAttribute(string fromDatePropertyName)
        {
            _fromDatePropertyName = fromDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_fromDatePropertyName);

            if (propertyInfo == null)
            {
                return new ValidationResult($"Unknown property: {_fromDatePropertyName}");
            }

            var fromDateValue = (DateTime?)propertyInfo.GetValue(validationContext.ObjectInstance);
            var toDateValue = (DateTime?)value;

            if (fromDateValue.HasValue && toDateValue.HasValue && fromDateValue.Value >= toDateValue.Value)
            {
                return new ValidationResult($"'{_fromDatePropertyName}' must be before '{validationContext.MemberName}'.");
            }

            return ValidationResult.Success;
        }
    }
}
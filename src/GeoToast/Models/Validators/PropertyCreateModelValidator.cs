using FluentValidation;

namespace GeoToast.Models.Validators
{
    public class PropertyCreateModelValidator : AbstractValidator<PropertyCreateModel>
    {
        public PropertyCreateModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();
        }
    }
}
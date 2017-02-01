using FluentValidation;

namespace GeoToast.Models.Validators
{
    public class LocationModelValidator : AbstractValidator<LocationModel>
    {
        public LocationModelValidator()
        {
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180);
        }
    }
}
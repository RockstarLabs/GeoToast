using FluentValidation;

namespace GeoToast.Models.Validators
{
    public class NotificationCreateModelValidator : AbstractValidator<NotificationCreateModel>
    {
        public NotificationCreateModelValidator()
        {
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.Location)
                .NotNull()
                .SetValidator(new LocationModelValidator());
            // RuleFor(x => x.Location.Longitude).NotEmpty().When(x => x.Location != null);
            // RuleFor(x => x.Location.Latitude).NotEmpty().When(x => x.Location != null);
        }
    }
}
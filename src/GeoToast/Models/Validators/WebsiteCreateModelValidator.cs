using FluentValidation;

namespace GeoToast.Models.Validators
{
    public class WebsiteCreateModelValidator : AbstractValidator<WebsiteCreateModel>
    {
        public WebsiteCreateModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();
        }
    }
}
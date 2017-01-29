using FluentValidation.TestHelper;
using GeoToast.Data.Models;
using GeoToast.Models;
using GeoToast.Models.Validators;
using Xunit;

namespace GeoToast.Tests.Models.Validators
{
    public class PropertyCreateModelValidatorTests
    {
        PropertyCreateModelValidator _validator;

        public PropertyCreateModelValidatorTests()
        {
            _validator = new PropertyCreateModelValidator();
        }

        [Fact]
        public void Should_have_error_when_name_is_null()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);
        }

        [Fact]
        public void Should_have_error_when_url_is_null_for_website()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Url, new PropertyCreateModel { Kind = PropertyKind.Website });
        }

        [Fact]
        public void Should_not_have_error_when_url_is_null_for_email()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Url, new PropertyCreateModel { Kind = PropertyKind.Email });
        }
    }
}
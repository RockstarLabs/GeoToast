using GeoToast.Models.Validators;
using Xunit;
using FluentValidation.TestHelper;

namespace GeoToast.Tests.Models.Validators
{
    public class LocationModelValidatorTests
    {
        LocationModelValidator _validator;

        public LocationModelValidatorTests()
        {
            _validator = new LocationModelValidator();
        }
        
        [Theory]
        [InlineData(90.1)]
        [InlineData(-90.1)]
        public void Should_have_error_when_latitude_not_in_range(float latitude)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Latitude, latitude);
        }

        [Theory]
        [InlineData(90)]
        [InlineData(-90)]
        public void Should_not_have_error_when_latitude_in_range(float latitude)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Latitude, latitude);
        }

        [Theory]
        [InlineData(180.1)]
        [InlineData(-180.1)]
        public void Should_have_error_when_longitude_not_in_range(float longitude)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Longitude, longitude);
        }

        [Theory]
        [InlineData(180)]
        [InlineData(-180)]
        public void Should_not_have_error_when_longitude_in_range(float longitude)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Longitude, longitude);
        }
    }
}
using System;
using FluentValidation.TestHelper;
using GeoToast.Models;
using GeoToast.Models.Validators;
using Xunit;

namespace GeoToast.Tests.Models.Validators
{
    public class NotificationCreateModelValidatorTests
    {
        NotificationCreateModelValidator _validator;

        public NotificationCreateModelValidatorTests()
        {
            _validator = new NotificationCreateModelValidator();
        }

        [Fact]
        public void Should_have_error_when_default_start_date()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.StartDate, DateTime.MinValue);
        }

        [Fact]
        public void Should_have_error_when_default_end_date()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.EndDate, DateTime.MinValue);
        }

        [Fact]
        public void Should_have_error_when_location_is_null()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Location, null as LocationModel);
        }
    }
}
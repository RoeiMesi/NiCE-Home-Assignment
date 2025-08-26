using System;
using FluentValidation.TestHelper;
using NiCE_Home_Assignment.Models.Domain;
using NiCE_Home_Assignment.Validation;
using Xunit;

namespace NiCE_Home_Assignment.Tests
{
    public class UserDetailsValidatorTest
    {
        private readonly UserDetailsValidator _validator = new();

        [Fact]
        public void Valid_Payload_Passes()
        {
                // Arrange
                var now = DateTime.UtcNow;
                var model = new UserDetails
                {
                    utterance = "forgot password",
                    userId = "123123",
                    sessionId = "321321",
                    timestamp = now
                };

                // Act
                var result = _validator.TestValidate(model);

                // Assert
                result.ShouldNotHaveAnyValidationErrors();

        }

        [Fact]
        public void Missing_Utterance_Fails()
        {
            var now = DateTime.UtcNow;
            var model = new UserDetails
            {
                utterance = "",
                userId = "123123",
                sessionId = "321321",
                timestamp = now
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.utterance);
        }

        [Fact]
        public void Future_Timestamp_Fails()
        {
            var futureTime = DateTime.UtcNow.AddMinutes(1);
            var model = new UserDetails
            {
                utterance = "forgot password",
                userId = "123123",
                sessionId = "321321",
                timestamp = futureTime
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(result => result.timestamp);
        }
    }
}

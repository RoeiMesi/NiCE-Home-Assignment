using System;
using System.Collections.Generic;
using FluentValidation;
using NiCE_Home_Assignment.Models.Domain;
using NiCE_Home_Assignment.Validation;
using NiCE_Home_Assignment.API.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Microsoft.AspNetCore.Mvc;


namespace NiCE_Home_Assignment.Tests.UnitTests
{
    public class SuggestClassControllerTest
    {
        [Fact]
        public async Task ResetPassword_Utterance_Returns_ResetPasswordTask()
        {
            IValidator<UserDetails> validator = new UserDetailsValidator();
            var logger = NullLogger<SuggestTaskController>.Instance;

            var controller = new SuggestTaskController(validator, logger);

            var taskDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                 { "reset password", "ResetPasswordTask" },
                 { "forgot password", "ResetPasswordTask" },
                 { "check order", "CheckOrderStatusTask" },
                 { "track order", "CheckOrderStatusTask" }
            };

            var model = new UserDetails
            {

                utterance = "reset password",
                userId = "123123",
                sessionId = "321321",
                timestamp = DateTime.UtcNow
            };

            var result = await controller.SuggestTask(model, taskDictionary);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = ok.Value!;

            var taskProp = value.GetType().GetProperty("task");
            Assert.NotNull(taskProp);

            var taskValue = Assert.IsType<string>(taskProp!.GetValue(value));
            Assert.Equal("ResetPasswordTask", taskValue);

        }
    }
}

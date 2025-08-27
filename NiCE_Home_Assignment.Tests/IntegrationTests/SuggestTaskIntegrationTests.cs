using System;
using System.Net.Http.Json;
using Xunit;
using NiCE_Home_Assignment.Models.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiCE_Home_Assignment;
using Microsoft.Extensions.Logging;

namespace NiCE_Home_Assignment.Tests.IntegrationTests
{
    public class SuggestTaskIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private sealed record TaskResponse(string task, DateTime timestamp);
        public SuggestTaskIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ResetPassword_Returns_ResetPasswordTask()
        {
            var model = new UserDetails
            {
                utterance = "I forgot password to my account",
                userId = "123321",
                sessionId = "123321",
                timestamp = DateTime.UtcNow
            };

            var response = await _client.PostAsJsonAsync("/suggestTask", model);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadFromJsonAsync<TaskResponse>();

            Assert.Equal("ResetPasswordTask", body!.task);
        }
    }
}

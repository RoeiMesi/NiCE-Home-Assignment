using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NiCE_Home_Assignment.Models.Domain;
using NiCE_Home_Assignment.Services;

namespace NiCE_Home_Assignment.API.Controllers
{
    [Route("suggestTask")]
    [ApiController]
    public class SuggestTaskController : ControllerBase
    {
        private IValidator<UserDetails> _validator;
        private ILogger<SuggestTaskController> _logger;
        private ExternalTaskService _externalService;

        public SuggestTaskController(IValidator<UserDetails> validator, ILogger<SuggestTaskController> logger, ExternalTaskService externalService)
        {
            _validator = validator;
            _logger = logger;
            _externalService = externalService;
        }

        [HttpPost]
        public async Task<IActionResult> SuggestTask([FromBody] UserDetails model, [FromServices] IDictionary<string, string> taskDictionary)
        {
            _logger.LogInformation("SuggestTask request: UserId={UserId}, SessionId={SessionId}, Utterance='{Utterance}'",
                model.userId, model.sessionId, model.utterance);

            var result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                _logger.LogWarning("Validation failed: {Errors}",
                    string.Join("; ", result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));

                return BadRequest(result.ToDictionary());
            }

            // Now we know that model.utterance is not empty
            var utterance = model.utterance;
            string task = "NoTaskFound"; // Default task, changes if task found.
            foreach (var key in taskDictionary.Keys)
            {
                if (utterance.Contains(key, StringComparison.OrdinalIgnoreCase))
                {
                    task = taskDictionary[key];
                    break;
                }
            }
            _logger.LogInformation("SuggestTask decision: UserId={UserId}, SessionId={SessionId}, Task={Task}",
                model.userId, model.sessionId, task);

            string externalTaskVal = _externalService.GetTaskFromExternalSystem(utterance);
            if (externalTaskVal == "Failed to get task from external dependency")
            {
                return BadRequest(externalTaskVal);
            }


            return Ok(new { task, timestamp = DateTime.UtcNow });
        }
    }
}

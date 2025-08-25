using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NiCE_Home_Assignment.Models.Domain;

namespace NiCEtask.API.Controllers
{
    [Route("suggestTask")]
    [ApiController]
    public class SuggestTaskController : ControllerBase
    {
        private IValidator<UserDetails> _validator;
        private ILogger<SuggestTaskController> _logger;

        public SuggestTaskController(IValidator<UserDetails> validator, ILogger<SuggestTaskController> logger)
        {
            _validator = validator;
            _logger = logger;
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
            string task = "NoTaskFound";
            foreach (var keyValPair in taskDictionary)
            {
                if (utterance.Contains(keyValPair.Key))
                {
                    task = keyValPair.Value;
                    break;
                }
            }
            _logger.LogInformation("SuggestTask decision: UserId={UserId}, SessionId={SessionId}, Task={Task}",
                model.userId, model.sessionId, task);

            return Ok(new { task = task, timestamp = DateTime.UtcNow });
        }
    }
}

using FluentValidation;
using NiCE_Home_Assignment.Models.Domain;

namespace NiCE_Home_Assignment.Validation

{
    public class UserDetailsValidator : AbstractValidator<UserDetails>
    {
        public UserDetailsValidator()
        {
            RuleFor(x => x.utterance)
                .Must(s => !string.IsNullOrWhiteSpace(s))
                .WithMessage("Utterance is required");
            RuleFor(x => x.userId)
                .Must(s => !string.IsNullOrWhiteSpace(s))
                .WithMessage("User id is required");
            RuleFor(x => x.sessionId)
                .Must(s => !string.IsNullOrWhiteSpace(s))
                .WithMessage("Session ID is required");
            RuleFor(x => x.timestamp)
                .LessThanOrEqualTo(_ => DateTime.UtcNow)
                .WithMessage("Timestamp cannot be in the future");
        }
    }
}

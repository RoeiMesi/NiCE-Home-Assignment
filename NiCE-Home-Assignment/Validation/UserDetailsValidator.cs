using FluentValidation;
using NiCE_Home_Assignment.Models.Domain;

namespace NiCE_Home_Assignment.Validation

{
    public class UserDetailsValidator : AbstractValidator<UserDetails>
    {
        public UserDetailsValidator()
        {
            RuleFor(x => x.utterance)
                .NotEmpty()
                .WithMessage("Utterance is required");
            RuleFor(x => x.userId).NotEmpty().WithMessage("User id is required");
            RuleFor(x => x.sessionId).NotEmpty().WithMessage("Session ID is required");
            RuleFor(x => x.timestamp).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Timestamp cannot be in the future");
        }
    }
}

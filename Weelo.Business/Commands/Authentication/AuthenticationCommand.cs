using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Weelo.Common.Generics;

namespace Weelo.Business.Commands.Authentication
{
    public class AuthenticationCommand : Base.CommandRequest<AuthenticationResponse>
    {
        [Required]
        [JsonPropertyName("IdentificationNumber")]
        public string IdentificationNumber { get; set; }
        [Required]
        [JsonPropertyName("BirthDay")]
        public string BirthDay { get; set; }
        [Required]
        [JsonPropertyName("ActualDate")]
        public DateTime ActualDate { get; set; }
    }

    public class AuthenticationResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }
    public class AuthenticationValidator : AbstractValidator<AuthenticationCommand>
    {
        public AuthenticationValidator()
        {
            RuleFor(request => request.IdentificationNumber)
                    .NotNull().NotEmpty().WithMessage("La identificación es requerida.")
                    .MinimumLength(4).WithMessage("La identificación es invalida.")
                    .Must(Validation.ValidateNumber).WithMessage("La identificación solo debe contener números.");
            RuleFor(request => request.BirthDay)
                    .NotNull().NotEmpty().WithMessage("La fecha de nacimiento es requerida.")
                    .MinimumLength(4).WithMessage("La fecha de nacimiento es invalida.");
        }
    }
}

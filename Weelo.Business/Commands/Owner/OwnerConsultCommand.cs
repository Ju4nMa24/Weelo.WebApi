using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Weelo.Common.Generics;

namespace Weelo.Business.Commands.Owner
{
    public class OwnerConsultCommand : Base.CommandRequest<OwnerConsultResponse>
    {
        [Required]
        [JsonPropertyName("IdentificationNumber")]
        public string IdentificationNumber { get; set; }
    }

    public class OwnerConsultResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("Owner")]
        public dynamic Owner { get; set; } 
    }
    public class OwnerConsultValidator : AbstractValidator<OwnerConsultCommand>
    {
        public OwnerConsultValidator()
        {
            RuleFor(request => request.IdentificationNumber)
                    .NotNull().NotEmpty().WithMessage("El número de identificación es requerido.")
                    .MaximumLength(15).WithMessage("La longitud del número de identificación es invalida.")
                    .MinimumLength(2).WithMessage("La longitud del número de identificación es invalida.");
        }
    }
}

using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Weelo.Common.Generics;

namespace Weelo.Business.Commands.Owner
{
    public class OwnerCommand : Base.CommandRequest<OwnerResponse>
    {
        [Required]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("IdentificationNumber")]
        public string IdentificationNumber { get; set; }
        [Required]
        [JsonPropertyName("Address")]
        public string Address { get; set; }
        [JsonPropertyName("Photo")]
        public string Photo { get; set; }
        [Required]
        [JsonPropertyName("Birthday")]
        public string Birthday { get; set; }
    }

    public class OwnerResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
    }
    public class OwnerValidator : AbstractValidator<OwnerCommand>
    {
        public OwnerValidator()
        {
            RuleFor(request => request.Name)
                    .NotNull().NotEmpty().WithMessage("El nombre del propietario es requerido.")
                    .MaximumLength(50).WithMessage("La longitud del nombre es invalida.")
                    .MinimumLength(2).WithMessage("La longitud del nombre es invalida.")
                    .Must(Validation.ValidateRegexWithRegexPersonNameAndNull).WithMessage("El nombre del propietario no es valido.");
            RuleFor(request => request.Address)
                    .NotNull().NotEmpty().WithMessage("La dirección es requerida.")
                    .MinimumLength(5).WithMessage("La Dirección es invalida.");
            RuleFor(request => request.IdentificationNumber.ToString())
                    .NotNull().NotEmpty().WithMessage("La identificación es requerida.")
                    .MinimumLength(4).WithMessage("La identificación es invalida.")
                    .Must(Validation.ValidateNumber).WithMessage("La identificación solo debe contener números.");
            RuleFor(request => request.Birthday.ToString())
                    .NotNull().NotEmpty().WithMessage("La fecha de nacimiento es requerida.")
                    .MinimumLength(4).WithMessage("La fecha de nacimiento es invalida.");
        }
    }
}

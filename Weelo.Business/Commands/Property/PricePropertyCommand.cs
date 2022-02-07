using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Weelo.Common.Generics;

namespace Weelo.Business.Commands.Property
{
    public class PricePropertyCommand : Base.CommandRequest<PricePropertyResponse>
    {
        [Required]
        [JsonPropertyName("CodeInternal")]
        public string CodeInternal { get; set; }
        [Required]
        [JsonPropertyName("Price")]
        public string Price { get; set; }
    }

    public class PricePropertyResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        [JsonPropertyName("Property")]
        public dynamic Property { get; set; }
    }
    public class PricePropertyValidator : AbstractValidator<PricePropertyCommand>
    {
        public PricePropertyValidator()
        {
            RuleFor(request => request.CodeInternal)
                    .NotNull().NotEmpty().WithMessage("El código interno es requerido.")
                    .MaximumLength(50).WithMessage("La longitud del código interno es invalida.")
                    .MinimumLength(2).WithMessage("La longitud del código interno es invalida.");
            RuleFor(request => request.Price.ToString())
                    .NotNull().NotEmpty().WithMessage("El precio es requerido.")
                    .MinimumLength(2).WithMessage("La longitud del precio es invalida.")
                    .Must(Validation.ValidateNumber).WithMessage("El precio solo debe tener números.");
        }
    }
}

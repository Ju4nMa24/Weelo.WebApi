using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Weelo.Common.Generics;

namespace Weelo.Business.Commands.Property
{
    public class PropertyPurchaseCommand : Base.CommandRequest<PropertyPurchaseResponse>
    {
        [Required]
        [JsonPropertyName("DateSale")]
        public DateTime DateSale { get; set; }
        [Required]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Value")]
        public double Value { get; set; }
        [JsonPropertyName("Tax")]
        public string Tax { get; set; }
        [JsonPropertyName("CodeInternal")]
        public string CodeInternal { get; set; }
    }

    public class PropertyPurchaseResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
    }
    public class PropertyPurchaseValidator : AbstractValidator<PropertyPurchaseCommand>
    {
        public PropertyPurchaseValidator()
        {
            RuleFor(request => request.Name)
                    .NotNull().NotEmpty().WithMessage("El nombre de la propiedad es requerido.")
                    .MaximumLength(50).WithMessage("La longitud del nombre es invalida.")
                    .MinimumLength(2).WithMessage("La longitud del nombre es invalida.")
                    .Must(Validation.ValidateRegexWithRegexPersonNameAndNull).WithMessage("El nombre de la propiedad no es valido.");
            RuleFor(request => request.Value.ToString())
                    .NotNull().NotEmpty().WithMessage("El valor es requerido.")
                    .MinimumLength(5).WithMessage("La longitud del valor es inferior a 4.")
                    .Must(Validation.ValidateNumber).WithMessage("El valor solo debe contener números.");
            RuleFor(request => request.Tax)
                    .NotNull().NotEmpty().WithMessage("El impuesto es requerido.");
            RuleFor(request => request.CodeInternal.ToString())
                    .NotNull().NotEmpty().WithMessage("El Código interno es requerido.")
                    .MinimumLength(1).WithMessage("El código interno es invalido.");
        }
    }
}

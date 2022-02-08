using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Weelo.Common.Generics;

namespace Weelo.Business.Commands.Property
{
    public class PropertyCommand : Base.CommandRequest<PropertyResponse>
    {
        [Required]
        [JsonPropertyName("PropertyName")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("Address")]
        public string Address { get; set; }
        [JsonPropertyName("Price")]
        public double Price { get; set; }
        [JsonPropertyName("CodeInternal")]
        public string CodeInternal { get; set; }
        [JsonIgnore]
        public string CodeInternalOrigin { get; set; }
        [Required]
        [JsonPropertyName("Year")]
        public string Year { get; set; }
        [JsonPropertyName("IdentificationNumner")]
        public string IdentificationNumner { get; set; }
        [JsonIgnore]
        public Guid IdProperty { get; set; }
        [JsonIgnore]
        public Guid IdOwner { get; set; }
    }

    public class PropertyResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("IdProperty")]
        public string IdProperty { get; set; }
    }

    public class PropertyValidator : AbstractValidator<PropertyCommand>
    {
        public PropertyValidator()
        {
            RuleFor(request => request.Name)
                    .NotNull().NotEmpty().WithMessage("El nombre de la propiedad es requerido.")
                    .MaximumLength(50).WithMessage("La longitud del nombre es invalida.")
                    .MinimumLength(2).WithMessage("La longitud del nombre es invalida.");
            RuleFor(request => request.Address)
                    .NotNull().NotEmpty().WithMessage("La dirección es requerida.")
                    .MinimumLength(5).WithMessage("La Dirección es invalida.");
            RuleFor(request => request.Price.ToString())
                    .NotNull().NotEmpty().WithMessage("El precio es requerido.")
                    .MinimumLength(4).WithMessage("El precio es invalido.")
                    .Must(Validation.ValidateNumber).WithMessage("El precio solo debe contener números.");
            RuleFor(request => request.CodeInternal.ToString())
                    .NotNull().NotEmpty().WithMessage("El Código interno es requerido.")
                    .MinimumLength(1).WithMessage("El código interno es invalido.");
            RuleFor(request => request.Year)
                    .NotNull().NotEmpty().WithMessage("El año de la propiedad es requerido.")
                    .MinimumLength(4).WithMessage("El año de la propiedad es invalido.")
                    .Must(Validation.ValidateNumber).WithMessage("El año de la propiedad solo debe contener números.");
        }
    }
    public class PropertyValidatorCreate : AbstractValidator<PropertyCommand>
    {
        public PropertyValidatorCreate()
        {
            RuleFor(request => request.CodeInternal.ToString())
                    .NotNull().NotEmpty().WithMessage("El Código interno es requerido.")
                    .MinimumLength(1).WithMessage("El código interno es invalido.");
            RuleFor(request => request.IdentificationNumner)
                    .NotNull().NotEmpty().WithMessage("El número de identificación es requerido.")
                    .MaximumLength(15).WithMessage("La longitud del número de identificación es invalida.")
                    .MinimumLength(2).WithMessage("La longitud del número de identificación es invalida.");
        }
    }
}

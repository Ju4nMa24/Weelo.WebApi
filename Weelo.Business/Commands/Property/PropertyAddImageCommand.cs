using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Weelo.Common.Generics;

namespace Weelo.Business.Commands.Property
{
    public class PropertyAddImageCommand : Base.CommandRequest<PropertyAddImageResponse>
    {
        [Required]
        [JsonPropertyName("CodeInternal")]
        public string CodeInternal { get; set; }
        [JsonPropertyName("Files")]
        public List<Files> Files { get; set; }
    }
    public class Files
    {
        [Required]
        [JsonPropertyName("FileUrl")]
        public string FileUrl { get; set; }
        [JsonPropertyName("Enabled")]
        public bool Enabled { get; set; }
    }
    public class PropertyAddImageResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
    }
    public class PropertyAddImageValidator : AbstractValidator<PropertyAddImageCommand>
    {
        public PropertyAddImageValidator()
        {
            RuleFor(request => request.CodeInternal)
                    .NotNull().NotEmpty().WithMessage("El código interno es requerido.")
                    .MaximumLength(50).WithMessage("La longitud del código interno es invalida.")
                    .MinimumLength(2).WithMessage("La longitud del código interno es invalida.");
        }
    }
}

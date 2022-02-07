using FluentValidation;
using System.Text.Json.Serialization;
using Weelo.Common.Generics;

namespace Weelo.Business.Commands.Property
{
    public class PropertyFiltersCommand : Base.CommandRequest<PropertyFiltersResponse>
    {
        [JsonPropertyName("PropertyName")]
        public string Name { get; set; }
        [JsonPropertyName("Address")]
        public string Address { get; set; }
        [JsonPropertyName("Price")]
        public double Price { get; set; }
        [JsonPropertyName("CodeInternal")]
        public string CodeInternal { get; set; }
        [JsonPropertyName("Year")]
        public string Year { get; set; }
    }
    public class PropertyFiltersResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Properties { get; set; }
    }
}

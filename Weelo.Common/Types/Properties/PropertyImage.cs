using System;
using System.ComponentModel.DataAnnotations;
using Weelo.Abstrations.Types.Properties;

namespace Weelo.Common.Types.Properties
{
    public class PropertyImage : IPropertyImage
    {
        [Key]
        public Guid IdPropertyImage { get; set; }
        public Guid IdProperty { get; set; }
        public string File { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Enabled { get; set; }
    }
}

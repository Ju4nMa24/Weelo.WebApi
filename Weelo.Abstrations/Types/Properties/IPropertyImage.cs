using System;
using System.ComponentModel.DataAnnotations;

namespace Weelo.Abstrations.Types.Properties
{
    public interface IPropertyImage
    {
        [Key]
        public Guid IdPropertyImage { get; set; }
        public Guid IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}

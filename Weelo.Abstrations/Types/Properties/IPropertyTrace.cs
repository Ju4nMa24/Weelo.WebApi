using System;
using System.ComponentModel.DataAnnotations;

namespace Weelo.Abstrations.Types.Properties
{
    public interface IPropertyTrace
    {
        [Key]
        public Guid IdPropertyTrace { get; set; }
        public Guid IdProperty { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Tax { get; set; }
    }
}

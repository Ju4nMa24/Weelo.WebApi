using System;
using System.ComponentModel.DataAnnotations;
using Weelo.Abstrations.Types.Properties;

namespace Weelo.Common.Types.Properties
{
    public class PropertyTrace : IPropertyTrace
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

using System;
using System.ComponentModel.DataAnnotations;
using Weelo.Abstrations.Types.Properties;

namespace Weelo.Common.Types.Properties
{
    public class Property : IProperty
    {
        [Key]
        public Guid IdProperty { get; set; }
        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public DateTime CreationDate { get; set; }
        public string Year { get; set; }
    }
}

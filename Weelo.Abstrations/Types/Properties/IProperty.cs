using System;
using System.ComponentModel.DataAnnotations;

namespace Weelo.Abstrations.Types.Properties
{
    public interface IProperty
    {
        [Key]
        public Guid IdProperty { get; set; }
        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
    }
}

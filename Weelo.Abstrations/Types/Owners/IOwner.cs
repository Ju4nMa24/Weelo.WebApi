using System;
using System.ComponentModel.DataAnnotations;

namespace Weelo.Abstrations.Types.Owners
{
    public interface IOwner
    {
        [Key]
        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Birthday { get; set; }
        public string IdentificationNumber { get; set; }
    }
}

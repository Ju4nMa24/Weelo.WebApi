using System;
using System.ComponentModel.DataAnnotations;
using Weelo.Abstrations.Types.Owners;

namespace Weelo.Common.Types.Owners
{
    public class Owner : IOwner
    {
        [Key]
        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime CreationDate { get; set; }
        public string Birthday { get; set; }
    }
}

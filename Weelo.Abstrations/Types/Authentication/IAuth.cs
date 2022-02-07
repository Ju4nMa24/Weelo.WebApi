using System;

namespace Weelo.Abstrations.Types.Authentication
{
    public interface IAuth
    {
        public string IdentificationNumber { get; set; }
        public string BirthDay { get; set; }
        public DateTime ActualDate { get; set; }
    }
}

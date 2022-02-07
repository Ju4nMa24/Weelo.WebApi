using System;
using Weelo.Abstrations.Types.Authentication;

namespace Weelo.Common.Types.Authentication
{
    public class Auth : IAuth
    {
        public string IdentificationNumber { get; set; }
        public string BirthDay { get; set; }
        public DateTime ActualDate { get; set; }
    }
}

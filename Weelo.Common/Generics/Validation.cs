using System;

namespace Weelo.Common.Generics
{
    public static class Validation
    {
        public static bool ValidateGuid(string value)
        {
            if (value == null) return false;
            return Guid.TryParse(value, out var _);
        }

        public static bool ValidateGuidAndNull(string value)
        {
            if (value == null) return true;
            return Guid.TryParse(value, out var _);
        }

        public static bool ValidateNumberAndNull(string value) => (value is not null && RegularExpressions.RegexPhone.IsMatch(value));

        public static bool ValidateBoolAndNull(string value)
        {
            if (value == null) return true;
            return bool.TryParse(value, out var _);
        }

        public static bool ValidateBool(string value)
        {
            if (value == null) return false;
            return bool.TryParse(value, out var _);
        }

        public static bool ValidateStringAndNull(string value) => (value is null || string.IsNullOrEmpty(value.Trim()));

        public static bool ValidateRegexWithSymbolAndNull(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;
            return RegularExpressions.RegexWithSymbol.IsMatch(value);
        }

        public static bool ValidateRegexWithRegexPersonNameAndNull(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;
            return RegularExpressions.RegexPersonName.IsMatch(value);
        }
        
        public static bool ValidateNumber(string value)
        {
            if (value == null) return false;
            return int.TryParse(value, out var _);
        }
        
        public static bool ValidateNumberCompostType(string value)
        {
            if (value == null) return false;
            if (value != "1" && value != "2") return false;
            return int.TryParse(value, out var _);
        }
        
        public static bool ValidateLongNumber(string value)
        {
            if (value == null) return false;
            return long.TryParse(value, out var _);
        }
        
        public static bool ValidateEmail(string value) => (RegularExpressions.RegexEmail.IsMatch(value));
    }
}

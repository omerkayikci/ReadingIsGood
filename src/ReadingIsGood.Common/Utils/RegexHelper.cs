using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReadingIsGood.Common.Utils
{
    public class RegexHelper
    {
        public static bool IsValidPhoneNumber(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var regex = new Regex(@"(([\+]90?)|([0]?))([ ]?)((\([0-9]{3}\))|([0-9]{3}))([ ]?)([0-9]{3})(\s*[\-]?)([0-9]{2})(\s*[\-]?)([0-9]{2})");

            return regex.IsMatch(value);
        }

        public static bool IsValidEmail(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

            return regex.IsMatch(value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_PASSWORD_MANAGER.Models
{
    internal class PasswordGeneratorModel
    {
        public static string GeneratePassword(int length, bool includeUppercase, bool includeLowercase, bool includeNumbers, bool includeSpecialChars)
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numberChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+[]{}|;:,.<>?";

            StringBuilder validChars = new StringBuilder();
            if (includeLowercase)
                validChars.Append(lowercaseChars);
            if (includeUppercase)
                validChars.Append(uppercaseChars);
            if (includeNumbers)
                validChars.Append(numberChars);
            if (includeSpecialChars)
                validChars.Append(specialChars);

            char[] password = new char[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomData = new byte[length];
                rng.GetBytes(randomData);
                for (int i = 0; i < length; i++)
                {
                    int index = randomData[i] % validChars.Length;
                    password[i] = validChars[index];
                }
            }

            return new string(password);
        }
    }
}

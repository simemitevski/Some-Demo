using Microsoft.AspNetCore.Identity;
using SecurityDemo.Services.Services.Definition;
using System.Collections.Generic;
using System.Linq;

namespace SecurityDemo.Services.Services.Implementation.Security
{
    public class PasswordGeneratorService : IPasswordGenerator
    {
        private const string UppercaseLatinLetters = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        private const string LowercaseLatinLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string NonAlphanumeric = "!@$%^&*()_?";

        private static readonly string AllCharacters = $"{UppercaseLatinLetters}{LowercaseLatinLetters}{Digits}{NonAlphanumeric}";

        public string Create()
        {
            var options = new PasswordOptions
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = false,
                RequireUppercase = true
            };

            var password = new List<char>();

            if (options.RequireUppercase)
            {
                password.Insert(RandomService.Next(password.Count), RandomUppercase());
            }

            if (options.RequireLowercase)
            {
                password.Insert(RandomService.Next(password.Count), RandomLowercase());
            }

            if (options.RequireDigit)
            {
                password.Insert(RandomService.Next(password.Count), RandomDigit());
            }

            if (options.RequireNonAlphanumeric)
            {
                password.Insert(RandomService.Next(password.Count), RandomNonAlphanumeric());
            }

            for (var i = password.Count; i < options.RequiredLength
                || password.Distinct().Count() < options.RequiredUniqueChars; i++)
            {
                password.Insert(RandomService.Next(password.Count), GetRandomChar(AllCharacters));
            }

            return new string(password.ToArray());
        }

        public char RandomUppercase() => GetRandomChar(UppercaseLatinLetters);

        public char RandomLowercase() => GetRandomChar(LowercaseLatinLetters);

        public char RandomDigit() => GetRandomChar(Digits);

        public char RandomNonAlphanumeric() => GetRandomChar(NonAlphanumeric);

        private char GetRandomChar(string possibleChars)
        {
            var index = RandomService.Next(possibleChars.Length);
            return possibleChars[index];
        }
    }
}

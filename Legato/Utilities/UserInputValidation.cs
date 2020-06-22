using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Legato.Models.PostModel;
using Legato.Models.UserModels;

namespace Legato.Utilities
{
    /// <summary>
    ///     Class to validate the user's input
    /// </summary>
    public class UserInputValidation
    {
        private readonly List<string> _musicGenres = new List<string>
        {
            "Pop",
            "Electronic/Dance",
            "Hip-Hop",
            "Rock",
            "Jazz",
            "R&B",
            "Soul",
            "Indie",
            "Classical",
            "Metal",
            "Latin",
            "Reggae",
            "Blues",
            "Funk",
            "Punk",
            "Country",
            "Folk & acoustic",
            "Desi",
            "Arab",
            "Afro",
            "K-Pop"
        };

        private readonly Post _post;

        private readonly UserRegistration _userRegistration;

        /// <summary>
        ///     Constructor for UserInputValidation class to validate registration inputs
        /// </summary>
        /// <param name="userRegistration">User registration model</param>
        public UserInputValidation(UserRegistration userRegistration)
        {
            _userRegistration = userRegistration;
        }

        /// <summary>
        ///     Constructor for UserInputValidation class to validate post inputs
        /// </summary>
        /// <param name="post">Post model</param>
        public UserInputValidation(Post post)
        {
            _post = post;
        }

        /// <summary>
        ///     Validates the user's inputs given at the registration
        /// </summary>
        /// <returns>Boolean value whether every value is valid or not</returns>
        public bool RegistrationIsValid()
        {
            try
            {
                ValidateName(_userRegistration.FirstName);
                ValidateName(_userRegistration.LastName);
                ValidateBirthdate(_userRegistration.Birthdate);
                ValidateCountry(_userRegistration.Country);
                ValidateGenres(_userRegistration.Genres);
                ValidatePassword(_userRegistration.Password);
                ValidateEmail(_userRegistration.Email);
                return true;
            }
            catch (ValidationException)
            {
                return false;
            }
        }

        private bool ValidateName(string name)
        {
            if (name == null) throw new ValidationException();

            var match = Regex.Match(name, @"^[\p{Lu}][\p{L}'-]+([ \p{L}][\p{L}'-]+)*$");

            if (!match.Success) throw new ValidationException();

            return true;
        }

        private bool ValidateBirthdate(string birthdate)
        {
            if (birthdate == null) throw new ValidationException();

            var match = Regex.Match(birthdate, @"^\d{4}\.\d{2}\.\d{2}$");

            if (!match.Success) throw new ValidationException();

            DateTime parsedBirthdate;

            try
            {
                parsedBirthdate = Converters.DateTimeConverter(birthdate);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ValidationException();
            }

            var currentDate = DateTime.Today;
            if (new DateTime(currentDate.Year - 14, currentDate.Month, currentDate.Day) < parsedBirthdate)
                throw new ValidationException();

            return true;
        }

        private bool ValidateCountry(string country)
        {
            if (country == null) throw new ValidationException();

            var countries = ApiCalls.GetCountries().Result;

            if (!countries.Contains(country)) throw new ValidationException();

            return true;
        }

        private bool ValidateGenres(List<string> genres)
        {
            if (genres == null) throw new ValidationException();

            if (!genres.All(genre => _musicGenres.Contains(genre))) throw new ValidationException();

            return true;
        }

        private bool ValidatePassword(string password)
        {
            if (password == null) throw new ValidationException();

            var match = Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

            if (!match.Success) throw new ValidationException();

            return true;
        }

        private bool ValidateEmail(string email)
        {
            if (email == null) throw new ValidationException();

            var match = Regex.Match(email,
                @"^(([^<>()\[\]\\.,;:\s@]+(\.[^<>()\[\]\\.,;:\s@]+)*)|(.+))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$");

            if (!match.Success) throw new ValidationException();

            return true;
        }
    }
}
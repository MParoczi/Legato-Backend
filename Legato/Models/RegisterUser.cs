using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Legato.Models
{
    /// <summary>
    ///     RegisterUser model which contains the necessary properties to register a user
    /// </summary>
    public class RegisterUser
    {
        /// <summary>
        ///     Fist name of the user to register
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name of the user to register
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        ///     Birthdate of the user to register
        /// </summary>
        [Required]
        public DateTime Birthdate { get; set; }

        /// <summary>
        ///     Home country of the user to register
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        ///     Favorite music genres of the user to register
        /// </summary>
        [Required]
        public List<string> Genres { get; set; }
    }
}
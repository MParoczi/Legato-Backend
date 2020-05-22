using System;
using System.Collections.Generic;

namespace Legato.Models
{
    /// <summary>
    ///     UserRegistration model which contains the necessary properties to register a user
    /// </summary>
    public class UserRegistration
    {
        /// <summary>
        ///     First name of the user to register
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name of the user to register
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Birthdate of the user to register
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        ///     Home country of the user to register
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     Favorite music genres of the user to register
        /// </summary>
        public List<string> Genres { get; set; }
    }
}
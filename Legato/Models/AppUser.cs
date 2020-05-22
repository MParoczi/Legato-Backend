using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Legato.Models
{
    /// <summary>
    ///     The main user model that is provided for the identity
    /// </summary>
    public class AppUser : IdentityUser<int>
    {
        /// <summary>
        ///     First name of the user to register
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name of the user to register
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public string LastName { get; set; }

        /// <summary>
        ///     Birthdate of the user to register
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public DateTime Birthdate { get; set; }

        /// <summary>
        ///     Home country of the user to register
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public string Country { get; set; }

        /// <summary>
        ///     Favorite music genres of the user to register
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public List<string> Genres { get; set; }
    }
}
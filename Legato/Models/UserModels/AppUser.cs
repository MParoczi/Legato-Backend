﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Legato.Models.PostModel;
using Microsoft.AspNetCore.Identity;

namespace Legato.Models.UserModels
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

        /// <summary>
        ///     JWT refresh token
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public string RefreshToken { get; set; }

        /// <summary>
        ///     Cloudinary URL for the user's profile picture
        /// </summary>
        [ProtectedPersonalData]
        public string ProfilePicture { get; set; }

        /// <summary>
        ///     Posts of the user
        /// </summary>
        [ProtectedPersonalData]
        public ICollection<Post> Posts { get; set; }
    }
}
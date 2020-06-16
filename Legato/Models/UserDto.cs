using System;
using System.Collections.Generic;

namespace Legato.Models
{
    /// <summary>
    ///     Simple POCO object to transfer user information to the frontend
    /// </summary>
    public class UserDto
    {
        /// <summary>
        ///     Constructor for the UserDto class to initialize a new transfer user object
        /// </summary>
        /// <param name="user">AppUser that is persisted in the database</param>
        public UserDto(AppUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BirthDate = user.Birthdate;
            Email = user.Email;
            Country = user.Country;
            Genres = user.Genres;
            ProfilePicture = user.ProfilePicture;
        }

        /// <summary>
        ///     Default constructor
        /// </summary>
        public UserDto()
        {
        }

        /// <summary>
        ///     Unique id of the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     First name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Date of birth of the user
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        ///     Email address of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Home country of the user
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     Favorite music genres of the user
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        ///     JWT Token
        /// </summary>
        public Jwt Token { get; set; }

        /// <summary>
        ///     Cloudinary URL for the user's profile picture
        /// </summary>
        public string ProfilePicture { get; set; }
    }
}
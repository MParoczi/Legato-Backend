using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Legato.Models
{
    /// <summary>
    ///     LoginUser model which contains the necessary properties to login a user
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        ///     Email address of the user to login
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public string Email { get; set; }

        /// <summary>
        ///     Password of the user to login
        /// </summary>
        [NotMapped]
        [ProtectedPersonalData]
        public string Password { get; set; }
    }
}
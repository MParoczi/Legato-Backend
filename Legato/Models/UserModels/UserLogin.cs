namespace Legato.Models.UserModels
{
    /// <summary>
    ///     UserLogin model which contains the necessary properties to login a user
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        ///     Email address of the user to login
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Password of the user to login
        /// </summary>
        public string Password { get; set; }
    }
}
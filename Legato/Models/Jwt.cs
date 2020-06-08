using System;

namespace Legato.Models
{
    /// <summary>
    ///     POCO object for the JWT
    /// </summary>
    public class Jwt
    {
        /// <summary>
        ///     Constructor for the Jwt model
        /// </summary>
        /// <param name="token">Token string</param>
        /// <param name="expiration">Expiration time of the token</param>
        public Jwt(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }

        /// <summary>
        ///     Token string
        /// </summary>
        public string Token { get; }

        /// <summary>
        ///     Expiration time of the token
        /// </summary>
        public DateTime Expiration { get; }
    }
}
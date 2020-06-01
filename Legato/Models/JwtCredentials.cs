﻿using System;

namespace Legato.Models
{
    /// <summary>
    ///     Contains constants that needs to configure the JWT token provider
    /// </summary>
    public class JwtCredentials
    {
        /// <summary>
        ///     Issuer of the JWT token
        /// </summary>
        public static readonly string Issuer = "Legato";

        /// <summary>
        ///     Audience of the JWT token
        /// </summary>
        public static readonly string Audience = "ApiUser";

        /// <summary>
        ///     Secret key for the JWT token
        /// </summary>
        public static readonly string Key = Environment.GetEnvironmentVariable("LEGATO_SECRET_KEY");
    }
}
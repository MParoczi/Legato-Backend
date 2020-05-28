﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Legato.Extensions;
using Legato.Models;
using Legato.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Legato.Controllers
{
    /// <summary>
    ///     Controller class that handles HTTP Requests for the registration, login and logout of the users
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        ///     Constructor for the AccountController class
        /// </summary>
        /// <param name="userManager">Provides the APIs for managing user in a persistence store.</param>
        /// <param name="signInManager">Provides the APIs for user sign in.</param>
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        ///     Provides the APIs for managing user in a persistence store.
        /// </summary>
        private UserManager<AppUser> UserManager { get; }

        /// <summary>
        ///     Provides the APIs for user sign in.
        /// </summary>
        private SignInManager<AppUser> SignInManager { get; }

        /// <summary>
        ///     Register the user with the information sent in the HTTP post request
        /// </summary>
        /// <param name="model">Simple POCO object that contains the necessary properties to register the user</param>
        /// <returns>Defines a contract that represents the result of an action method.</returns>
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegistration model)
        {
            var response = new Response();
            var validator = new UserInputValidation(model);

            if (!ModelState.IsValid || !validator.RegistrationIsValid())
            {
                response.Message = "Registration data is in invalid form";
                return BadRequest(response);
            }


            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = model.Email,
                    Birthdate = Converters.DateTimeConverter(model.Birthdate),
                    Country = model.Country,
                    Email = model.Email,
                    Genres = new List<string>(model.Genres),
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await UserManager.CreateAsync(user, model.Password);
            }
            else
            {
                response.Message = "There is a registration under this email account";
                return BadRequest(response);
            }

            response.Message = "Registration was successful";
            return Ok(response);
        }
    }
}
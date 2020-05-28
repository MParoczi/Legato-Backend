using System.Collections.Generic;
using System.Threading.Tasks;
using EmailService;
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
        /// <param name="emailSender">The API for automatized e-mail handling</param>
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IEmailSender emailSender)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            EmailSender = emailSender;
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
        ///     Provides the APIs for sending automatized e-mails. In this case used for registration confirmation.
        /// </summary>
        public IEmailSender EmailSender { get; }

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

                if (result.Succeeded)
                {
                    var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink =
                        Url.Action("ConfirmEmail", "Account", new {userEmail = user.Email, token},
                            Request.Scheme);

                    var emailContent = EmailSender.CreateEmailContent(user.FirstName, confirmationLink);
                    var message = new Message(new[] {user.Email}, "Confirmation letter - Legato",
                        emailContent);

                    await EmailSender.SendEmailAsync(message);
                }
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
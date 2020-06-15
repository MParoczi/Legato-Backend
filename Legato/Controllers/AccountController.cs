using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmailService;
using Legato.Contexts.Contracts;
using Legato.Models;
using Legato.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        /// <param name="repository">Repository pattern access point</param>
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IEmailSender emailSender, IRepositoryWrapper repository)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            EmailSender = emailSender;
            Repository = repository;
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
        ///     Repository for the database context
        /// </summary>
        public IRepositoryWrapper Repository { get; }

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
                user.RefreshToken = CreateRefreshToken(user);
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

        /// <summary>
        ///     Login the user with the information sent in the HTTP post request
        /// </summary>
        /// <param name="model">Simple POCO object that contains the necessary properties to login the user</param>
        /// <returns>Defines a contract that represents the result of an action method</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin model)
        {
            var response = new Response();

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    response.Message = "There is no registration under this email address";
                    return BadRequest(response);
                }

                if (!user.EmailConfirmed)
                {
                    response.Message = "Email address is not confirmed";
                    return BadRequest(response);
                }

                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded)
                {
                    var loggedInUser = new UserDto(user);

                    loggedInUser.Token = CreateJwtPayload(user);

                    response.Message = "Login was successful";
                    response.Payload = loggedInUser;

                    HttpContext.Response.Cookies.Append("REFRESH_TOKEN", user.RefreshToken,
                        new CookieOptions {HttpOnly = true, Expires = DateTimeOffset.MaxValue});

                    return Ok(response);
                }
            }

            response.Message = "Login attempt has failed (invalid username of password)";
            return BadRequest(response);
        }

        /// <summary>
        ///     Logout the user
        /// </summary>
        /// <param name="model">Simple POCO object that represents the logged in user</param>
        /// <returns>Simple POCO object that contains the necessary properties to logout the user</returns>
        [HttpPost]
        public async Task<IActionResult> Logout([FromBody] UserDto model)
        {
            var response = new Response();

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                await UserManager.UpdateSecurityStampAsync(user);

                await SignInManager.SignOutAsync();
                response.Message = "Logout was successful";

                HttpContext.Response.Cookies.Delete("REFRESH_TOKEN");

                return Ok(response);
            }

            response.Message = "Logout attempt has failed";
            return BadRequest(response);
        }

        /// <summary>
        ///     Refreshes the invalid JWT token and refreshes the refresh token as well
        /// </summary>
        /// <returns>Defines a contract that represents the result of an action method</returns>
        [HttpPost]
        public async Task<IActionResult> RefreshToken()
        {
            var response = new Response();

            var refreshToken = HttpContext.Request.Cookies.FirstOrDefault(cookie => cookie.Key == "REFRESH_TOKEN")
                .Value;

            if (refreshToken != null)
            {
                var user = Repository.User.FindByCondition(u => u.RefreshToken == refreshToken).FirstOrDefault();

                if (user != null)
                {
                    var token = CreateJwtPayload(user);
                    var newRefreshToken = CreateRefreshToken(user);

                    response.Message = "New token was given";
                    response.Payload = token;

                    user.RefreshToken = newRefreshToken;
                    Repository.User.Update(user);
                    await Repository.Save();

                    HttpContext.Response.Cookies.Append("REFRESH_TOKEN", newRefreshToken,
                        new CookieOptions {HttpOnly = true, Expires = DateTimeOffset.MaxValue});

                    return Ok(response);
                }
            }

            response.Message = "Couldn't refresh the token";
            return Unauthorized(response);
        }

        /// <summary>
        ///     Checks whether there is a logged in user and return a new UserDto object for the frontend
        /// </summary>
        /// <returns>A simple HTTP response whether the current user is founded or not</returns>
        [HttpPost]
        public IActionResult GetCurrentUser()
        {
            var response = new Response();
            var refreshToken = HttpContext.Request.Cookies.FirstOrDefault(cookie => cookie.Key == "REFRESH_TOKEN")
                .Value;

            if (refreshToken != null)
            {
                var user = Repository.User.FindByCondition(u => u.RefreshToken == refreshToken).FirstOrDefault();

                if (user != null)
                {
                    var loggedInUser = new UserDto(user);

                    loggedInUser.Token = CreateJwtPayload(user);

                    response.Message = "Welcome back!";
                    response.Payload = loggedInUser;

                    return Ok(response);
                }
            }

            response.Message = "Logged in user was not found";
            return Unauthorized(response);
        }

        /// <summary>
        ///     Controls the registration confirmation. It redirects to the HarMoney frontend if the confirmation was successful.
        /// </summary>
        /// <param name="userEmail">The user's e-mail address where the service has sent the confirmation letter</param>
        /// <param name="token">The token that validates the registration</param>
        /// <returns>If the confirmation was successful, the controller will redirect to the HarMoney frontend</returns>
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userEmail, string token)
        {
            if (userEmail == null || token == null) return BadRequest();

            var user = await UserManager.FindByEmailAsync(userEmail);

            if (user == null) return BadRequest();

            await UserManager.ConfirmEmailAsync(user, token);
            return Redirect(Environment.GetEnvironmentVariable("LEGATO_FRONTEND"));
        }

        private JwtSecurityToken CreateToken(AppUser model)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtCredentials.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Email)
            };

            var token = new JwtSecurityToken(JwtCredentials.Issuer, JwtCredentials.Audience, claims,
                expires: DateTime.UtcNow.AddMinutes(15), signingCredentials: credentials);

            return token;
        }

        private Jwt CreateJwtPayload(AppUser user)
        {
            var jwt = CreateToken(user);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
            var tokenExpiration = jwt.ValidTo;
            var token = new Jwt(tokenString, tokenExpiration);
            return token;
        }

        private string CreateRefreshToken(AppUser user)
        {
            return new JwtSecurityTokenHandler().WriteToken(CreateToken(user));
        }
    }
}
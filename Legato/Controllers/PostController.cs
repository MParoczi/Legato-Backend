﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Legato.Contexts.Contracts;
using Legato.Models.PostModel;
using Legato.Models.UserModels;
using Legato.Models.UtilityModels;
using Legato.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Legato.Controllers
{
    /// <summary>
    ///     Controller class that handles HTTP Requests for the posts of the users
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        /// <summary>
        ///     Constructor for the PostController class
        /// </summary>
        /// <param name="repository">Repository pattern access point</param>
        public PostController(IRepositoryWrapper repository)
        {
            Repository = repository;
        }

        /// <summary>
        ///     Repository for the database context
        /// </summary>
        private IRepositoryWrapper Repository { get; }

        /// <summary>
        ///     Get every posts of a user that is persisted in the database
        /// </summary>
        /// <returns>Every posts of a user from the database</returns>
        /// <remarks>GET: api/Post/GetUserPosts</remarks>
        [HttpPost]
        public IActionResult GetUserPosts([FromBody] UserDto model)
        {
            var response = new Response();

            if (UserIsValid(model.Id))
            {
                response.Message = "All posts are selected";
                response.Payload = Repository.Post.FindByCondition(p => p.UserId.Equals(model.Id))
                    .OrderByDescending(p => p.DateOfCreation);

                return Ok(response);
            }

            response.Message = "The requesting user is not matching with the authorized one";
            return BadRequest(response);
        }

        /// <summary>
        ///     Gets one specified post
        /// </summary>
        /// <param name="id">Id of the post</param>
        /// <returns>One specified post</returns>
        /// <remarks>GET: api/Post/5</remarks>
        [HttpGet("{id}")]
        public IActionResult GetPost([FromQuery] int id)
        {
            var response = new Response();
            var post = Repository.Post.FindByCondition(p => p.Id.Equals(id));

            if (post == null)
            {
                response.Message = "Post was not found";
                return BadRequest(response);
            }

            response.Message = "Post was found";
            response.Payload = post;
            return Ok(response);
        }

        /// <summary>
        ///     Update a specified post
        /// </summary>
        /// <param name="model">Post to update</param>
        /// <param name="id">Id of the post</param>
        /// <returns>Defines a contract that represents the result of an action method</returns>
        /// <remarks>PUT: api/Post/5</remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Post model)
        {
            var response = new Response();
            var validator = new UserInputValidation(model);

            if (!UserIsValid(model.UserId))
            {
                response.Message = "The posting user is not matching with the authorized one";
                return BadRequest(response);
            }

            if (Repository.Post.FindByCondition(p => p.Id.Equals(id)).FirstOrDefault() == null)
            {
                response.Message = "Post was not found";
                return BadRequest(response);
            }

            if (!ModelState.IsValid || !validator.PostIsValid())
            {
                response.Message = "Post is in invalid form";
                return BadRequest(response);
            }

            model.DateOfEdit = DateTime.Now;
            
            Repository.Post.Update(model);
            await Repository.Save();


            response.Message = "Post was updated";
            response.Payload = model;
            return Ok(response);
        }

        /// <summary>
        ///     Add a new post to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Defines a contract that represents the result of an action method</returns>
        /// <remarks>POST: api/Post/Add</remarks>
        [HttpPost]
        public async Task<ActionResult<Post>> Add([FromBody] Post model)
        {
            var response = new Response();
            var validator = new UserInputValidation(model);

            if (!UserIsValid(model.UserId))
            {
                response.Message = "The posting user is not matching with the authorized one";
                return BadRequest(response);
            }

            if (!ModelState.IsValid || !validator.PostIsValid())
            {
                response.Message = "Post is in invalid form";
                return BadRequest(response);
            }

            model.DateOfCreation = DateTime.Now;

            Repository.Post.Create(model);
            await Repository.Save();

            response.Message = "Post was added";
            response.Payload = model;
            return Ok(response);
        }

        /// <summary>
        ///     Delete a specified post by its id
        /// </summary>
        /// <param name="id">Id of the specified post</param>
        /// <returns>Defines a contract that represents the result of an action method</returns>
        /// <remarks>DELETE: api/Post/5</remarks>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            var response = new Response();

            var post = Repository.Post.FindByCondition(p => p.Id.Equals(id)).FirstOrDefault();
            if (post == null)
            {
                response.Message = "Post was not found";
                return BadRequest(response);
            }

            if (!UserIsValid(post.UserId))
            {
                response.Message = "The deleting user is not matching with the authorized one";
                return BadRequest(response);
            }

            Repository.Post.Delete(post);
            await Repository.Save();

            response.Message = "Post was deleted";
            response.Payload = post;

            return Ok(response);
        }

        private bool UserIsValid(int id)
        {
            var jwt = HttpContext.Request.Headers["Authorization"][0];

            if (jwt == null) return false;

            jwt = jwt.Replace("Bearer ", "");

            var jwtHandler = new JwtSecurityTokenHandler();
            var result = jwtHandler.ReadToken(jwt) as JwtSecurityToken;

            var userId = result?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value;

            return userId != null && Convert.ToInt32(userId).Equals(id);
        }
    }
}
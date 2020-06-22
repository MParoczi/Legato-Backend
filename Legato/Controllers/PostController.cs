﻿using System.Linq;
using System.Threading.Tasks;
using Legato.Contexts.Contracts;
using Legato.Models.PostModel;
using Legato.Models.UtilityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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
        ///     Get every posts that is persisted in the database
        /// </summary>
        /// <returns>Every posts from the database</returns>
        /// <remarks>GET: api/Post</remarks>
        [HttpGet]
        public IActionResult Get()
        {
            var response = new Response {Message = "All posts are selected", Payload = Repository.Post.FindAll()};

            return Ok(response);
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
        /// <param name="post">Post to update</param>
        /// <returns>Defines a contract that represents the result of an action method</returns>
        /// <remarks>PUT: api/Post/5</remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Post post)
        {
            var response = new Response();
            if (Repository.Post.FindByCondition(p => p.Equals(post)).FirstOrDefault() == null)
            {
                response.Message = "Post was not found";
                return BadRequest(response);
            }

            try
            {
                Repository.Post.Update(post);
                await Repository.Save();
            }
            catch (SqlException e)
            {
                response.Message = e.ToString();
                return BadRequest(response);
            }

            response.Message = "Post was updated";
            response.Payload = post;
            return Ok(response);
        }

        /// <summary>
        ///     Add a new post to the database
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Defines a contract that represents the result of an action method</returns>
        /// <remarks>POST: api/Post</remarks>
        [HttpPost]
        public async Task<ActionResult<Post>> Add([FromBody] Post post)
        {
            var response = new Response();

            Repository.Post.Create(post);
            await Repository.Save();

            response.Message = "Post was added";
            response.Payload = post;
            return Ok(response);
        }

        /// <summary>
        ///     Delete a specified post by its id
        /// </summary>
        /// <param name="id">Id of the specified post</param>
        /// <returns>Defines a contract that represents the result of an action method</returns>
        /// <remarks>DELETE: api/Post/5</remarks>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete([FromQuery] int id)
        {
            var response = new Response();

            var post = Repository.Post.FindByCondition(p => p.Id.Equals(id)).FirstOrDefault();
            if (post == null)
            {
                response.Message = "Post was not found";
                return BadRequest(response);
            }

            Repository.Post.Delete(post);
            await Repository.Save();

            response.Message = "Post was deleted";
            response.Payload = post;

            return Ok(response);
        }
    }
}
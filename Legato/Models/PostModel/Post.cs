using System;
using Legato.Models.UserModels;

namespace Legato.Models.PostModel
{
    /// <summary>
    ///     POCO class to represent a post created by a user
    /// </summary>
    public class Post
    {
        /// <summary>
        ///     Unique identification for a single post
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Title of the post
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Content of the post
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     Date of the creation of the post
        /// </summary>
        public DateTime DateOfCreation { get; set; }

        /// <summary>
        ///     Id of the user that created the post
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Navigation property of the user
        /// </summary>
        public AppUser User { get; set; }
    }
}
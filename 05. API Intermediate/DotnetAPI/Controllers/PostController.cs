using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    // Specifies that all endpoints in this controller require authorization.
    [Authorize]
    // Indicates that this class is an API controller, providing behavior required for handling HTTP requests.
    [ApiController]
    // Defines the route template for the controller. In this case, the route will be based on the controller name, which is "Post".
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        
        // Initializes a new instance of the PostController class.
        public PostController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        // Retrieves all posts.
        [HttpGet("Posts")]
        public IEnumerable<Post> GetPost()
        {
            // SQL query to select all posts.
            string sql = @$"SELECT [PostId],
                            [UserId],
                            [PostTitle],
                            [PostContent],
                            [PostCreated],
                            [PostUpdated] 
                        FROM TutorialAppSchema.Posts";

            return _dapper.LoadData<Post>(sql);
        }

        // Retrieves a single post by its ID.
        [HttpGet("PostSingle/{postId}")]
        public Post GetPostSingle(int postId)
        {
            // SQL query to select a single post by ID.
            string sql = @$"SELECT [PostId],
                            [UserId],
                            [PostTitle],
                            [PostContent],
                            [PostCreated],
                            [PostUpdated] 
                        FROM TutorialAppSchema.Posts
                        WHERE PostId = {postId.ToString()}";
            return _dapper.LoadDataSingle<Post>(sql);
        }

        // Retrieves all posts by a specific user.
        [HttpGet("PostsByUser/{userId}")]
        public IEnumerable<Post> GetPostsByUSer(int userId)
        {
            // SQL query to select posts by a specific user.
            string sql = @$"SELECT [PostId],
                            [UserId],
                            [PostTitle],
                            [PostContent],
                            [PostCreated],
                            [PostUpdated] 
                        FROM TutorialAppSchema.Posts
                        WHERE UserId = {userId.ToString()}";
            return _dapper.LoadData<Post>(sql);
        }

        // Retrieves posts created by the currently authenticated user.
        [HttpGet("MyPosts")]
        public IEnumerable<Post> MyPosts()
        {
            // SQL query to select posts created by the current user.
            string sql = @$"SELECT [PostId],
                            [UserId],
                            [PostTitle],
                            [PostContent],
                            [PostCreated],
                            [PostUpdated] 
                        FROM TutorialAppSchema.Posts
                        WHERE UserId = {this.User.FindFirst("userId")?.Value}";

            return _dapper.LoadData<Post>(sql);
        }

        // Adds a new post.
        [HttpPost("Post")]
        public IActionResult AddPost(PostToAddDTO postToAdd)
        {
            // SQL query to insert a new post.
            string sql = @$"INSERT INTO TutorialAppSchema.Posts (
                            [UserId],
                            [PostTitle],
                            [PostContent],
                            [PostCreated],
                            [PostUpdated] 
                        ) VALUES (
                            {this.User.FindFirst("userId")?.Value},
                            '{postToAdd.PostTitle}',
                            '{postToAdd.PostContent}',
                            GETDATE(),
                            GETDATE()
                        )";
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to create new post!");
        }

        // Edits an existing post.
        [HttpPut("Post")]
        public IActionResult EditPost(PostToEditDTO postToEdit)
        {
            // SQL query to update an existing post.
            string sql = @$"UPDATE TutorialAppSchema.Posts 
                            SET PostTitle = '{postToEdit.PostTitle}', 
                                PostContent = '{postToEdit.PostContent}',
                                PostUpdated = GETDATE()
                            WHERE PostId = {postToEdit.PostId.ToString()}
                            AND UserId = {this.User.FindFirst("userId")?.Value}";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to edit post!");
        }

        // Deletes a post by its ID.
        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            // SQL query to delete a post by ID.
            string sql = @$"DELETE FROM TutorialAppSchema.Posts WHERE PostId = {postId.ToString()} AND UserId = {this.User.FindFirst("userId")?.Value}";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to delete post!");
        }

        /// <summary>
        /// Retrieves posts based on a search parameter.
        /// </summary>
        /// <param name="searchParam">The search parameter to filter posts by.</param>
        /// <returns>An enumerable collection of posts matching the search parameter.</returns>
        [HttpGet("PostsBySearch/{searchParam}")]
        public IEnumerable<Post> PostsBySearch(string searchParam)
        {
            // SQL query to select posts based on a search parameter, searching within post titles and content.
            string sql = @$"SELECT [PostId],
                            [UserId],
                            [PostTitle],
                            [PostContent],
                            [PostCreated],
                            [PostUpdated]
                        FROM TutorialAppSchema.Posts
                        WHERE PostTitle LIKE '%{searchParam}%'
                        OR PostContent LIKE '%{searchParam}%'";

            return _dapper.LoadData<Post>(sql);
        }
    }
}
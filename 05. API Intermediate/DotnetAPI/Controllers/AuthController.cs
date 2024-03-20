using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Controllers
{
    // Specifies that the controller requires authorization, meaning only authenticated users can access its endpoints.
    [Authorize]
    // Indicates that this class is an API controller, providing behavior required for handling HTTP requests.
    [ApiController]
    // Defines the route template for the controller. In this case, the route will be based on the controller name, which is "Auth".
    [Route("[controller]")]
    /// <summary>
    /// Controller handling authentication operations such as user registration and login.
    /// </summary>
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        private readonly AuthHelper _authHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="config">The application configuration.</param>
        public AuthController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _authHelper = new AuthHelper(config);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userForRegstration">User registration details.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [AllowAnonymous]
        // Allows unauthenticated access to the Register endpoint, enabling users to register without requiring authentication.
        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDTO userForRegstration)
        {
            // Check if passwords match
            if (userForRegstration.Password == userForRegstration.PasswordConfirm)
            {
                // Check if user already exists
                string sqlCheckUserExists = @$"SELECT Email FROM TutorialAppSchema.Auth
                                                    WHERE Email = '{userForRegstration.Email}'";

                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);

                if (existingUsers.Count() == 0)
                {
                    // Generate password salt and hash
                    byte[] passwordSalt = new byte[128 / 8];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordSalt);
                    }

                    byte[] passwordHash = _authHelper.GetPasswordHash(userForRegstration.Password, passwordSalt);

                    // Add user authentication details to database
                    string sqlAddAuth = @$"INSERT INTO TutorialAppSchema.Auth (
                                            [Email],
                                            [PasswordHash],
                                            [PasswordSalt]
                                        ) VALUES (
                                            '{userForRegstration.Email}',
                                            @PasswordHash, @PasswordSalt
                                        )";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
                    passwordSaltParameter.Value = passwordSalt;

                    SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
                    passwordHashParameter.Value = passwordHash;

                    sqlParameters.Add(passwordSaltParameter);
                    sqlParameters.Add(passwordHashParameter);

                    if (_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                    {
                        // Add user details to database
                        string sqlAddUser = @$"
                                    INSERT INTO TutorialAppSchema.Users(
                                        [FirstName],
                                        [LastName],
                                        [Email],
                                        [Gender],
                                        [Active]
                                    ) VALUES (
                                        '{userForRegstration.FirstName}',
                                        '{userForRegstration.LastName}',
                                        '{userForRegstration.Email}',
                                        '{userForRegstration.Gender}',
                                        'True'
                                    )";
                        if (_dapper.ExecuteSql(sqlAddUser))
                        {
                            return Ok();
                        }
                        throw new Exception("Failed to Add user.");
                    }
                    throw new Exception("Failed to Register user.");
                }
                throw new Exception("User with this email already exists!");
            }
            throw new Exception("Passwords do not match!");
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="userForLogin">User login details.</param>
        /// <returns>HTTP response containing authentication token on success, or error message on failure.</returns>
        [AllowAnonymous]
        // Allows unauthenticated access to the Login endpoint, enabling users to login without requiring authentication.
        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDTO userForLogin)
        {
            // Retrieve user's password hash and salt
            string sqlForHashAndSalt = @$"SELECT [PasswordHash],
                                            [PasswordSalt] 
                                        FROM TutorialAppSchema.Auth 
                                        WHERE Email = '{userForLogin.Email}'";

            UserForLoginConfirmationDTO userForConfirmation = _dapper
                .LoadDataSingle<UserForLoginConfirmationDTO>(sqlForHashAndSalt);

            
            // Generate password hash and compare with stored hash
            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, userForConfirmation.PasswordSalt);

            // if (passwordHash == userForConfirmation.PasswordHash) // Won't work
            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != userForConfirmation.PasswordHash[index])
                {
                    return StatusCode(401, "Incorrect Password");
                }
            }

            // Retrieve user ID and create JWT token
            string userIdSql = $@"SELECT UserId FROM TutorialAppSchema.Users WHERE Email = '{userForLogin.Email}'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(userId)}
            });
        }

        [HttpGet("RefreshToken")]
        // Endpoint for refreshing authentication token.
        public string RefreshToken()
        {
            // Retrieve the user ID from the authenticated user's claims.
            string userIdSql = $@"SELECT UserId FROM TutorialAppSchema.Users WHERE UserId = '{User.FindFirst("userId")?.Value}'";

            // Load user ID from the database.
            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            // Generate and return a new JWT token for the user.
            return _authHelper.CreateToken(userId);
        }
    }
}
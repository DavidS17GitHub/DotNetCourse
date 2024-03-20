using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Controllers
{
    /// <summary>
    /// Controller handling authentication operations such as user registration and login.
    /// </summary>
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="config">The application configuration.</param>
        public AuthController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _config = config;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userForRegstration">User registration details.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
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

                    byte[] passwordHash = GetPasswordHash(userForRegstration.Password, passwordSalt);

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
            byte[] passwordHash = GetPasswordHash(userForLogin.Password, userForConfirmation.PasswordSalt);

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
                {"token", CreateToken(userId)}
            });
        }

        /// <summary>
        /// Generates a password hash.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="passwordSalt">The salt for the password.</param>
        /// <returns>The password hash.</returns>
        private byte[] GetPasswordHash(string password, byte[] passwordSalt)
        {
            string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value +
                        Convert.ToBase64String(passwordSalt);

            byte[] passwordHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 258 / 8
            );

            return passwordHash;
        }

        /// <summary>
        /// Creates a JWT token.
        /// </summary>
        /// <param name="userId">The user ID to include in the token.</param>
        /// <returns>The JWT token.</returns>
        private string CreateToken(int userId)
        {
            Claim[] claims = new Claim[] {
                new Claim("userId", userId.ToString())
            };

            string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;

            SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        tokenKeyString != null ? tokenKeyString : ""
                    )
            );

            SigningCredentials credentials = new SigningCredentials(
                tokenKey,
                SecurityAlgorithms.HmacSha512Signature
            );

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(1)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace GamersHub.Api.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly DataContext _dataContext;

        public IdentityService(
            UserManager<IdentityUser> userManager,
            JwtSettings jwtSettings,
            DataContext dataContext)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _dataContext = dataContext;
        }

        public bool UserWithEmailExists(string email) => _dataContext.Users.Any(x => x.Email == email);

        public bool UserWithUsernameExists(string username) => _dataContext.Users.Any(x => x.UserName == username);

        public async Task<AuthenticationResult> LoginAsync(
            string login,
            string password)
        {
            var user = await _userManager.FindByEmailAsync(login);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(login);

                if (user == null)
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] { "User does not exist." }
                    };
                }
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Password does not match login." }
                };
            }

            return GenerateAuthResultForUser(user);
        }

        public async Task<AuthenticationResult> RegisterAsync(
            string email,
            string password,
            string username)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email already exists." }
                };
            }

            if (string.IsNullOrEmpty(username))
            {
                username = email;
            }
            else
            {
                existingUser = await _userManager.FindByNameAsync(username);

                if (existingUser != null)
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] { "User with this username already exists." }
                    };
                }
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = username
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return GenerateAuthResultForUser(newUser);
        }

        private AuthenticationResult GenerateAuthResultForUser(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}

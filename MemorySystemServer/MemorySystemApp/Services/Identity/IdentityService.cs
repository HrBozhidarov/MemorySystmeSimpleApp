namespace MemorySystemApp.Services.Identity
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;

    using MemorySystemApp.Data.Models;
    using MemorySystemApp.Models.Identity;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityService : IIdentityService
    {
        private const string DefaultProfileUrl = "https://cdn1.iconfinder.com/data/icons/technology-devices-2/100/Profile-512.png";

        private readonly UserManager<User> userManager;
        private readonly ApplicationSettings applicationSettings;

        public IdentityService(
            UserManager<User> userManager,
            IOptions<ApplicationSettings> options)
        {
            this.userManager = userManager;
            this.applicationSettings = options.Value;
        }

        public async Task<Result<LoginModel>> Login(LoginUserRequestModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = await this.userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Result<LoginModel>.Error("Username or password are invalid");
            }

            var validationResult = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!validationResult)
            {
                return Result<LoginModel>.Error("Username or password are invalid");
            }

            return Result<LoginModel>.Success(
                new LoginModel
                {
                    ProfileUrl = user.ProfileUrl,
                    Token = this.GenerateJwtToken(user),
                });
        }

        public async Task<Result<User>> Register(RegisterUserRequestModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException(nameof(model));
            }

            if (!string.IsNullOrWhiteSpace(model.ProfileUrl) && !Uri.IsWellFormedUriString(model.ProfileUrl, UriKind.RelativeOrAbsolute))
            {
                return Result<User>.Error("Invalid profile url");
            }
            else if (string.IsNullOrWhiteSpace(model.ProfileUrl))
            {
                model.ProfileUrl = DefaultProfileUrl;
            }

            var user = Mapper.Map<User>(model);

            var identityResult = await this.userManager.CreateAsync(user, model.Password);

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? Result<User>.Success(user)
                : Result<User>.Error(errors.First());
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.applicationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

namespace MemorySystemApp.Services.Identity
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using MemorySystemApp.Data.Models;
    using MemorySystemApp.Infrastructures.AutomapperSettings;
    using MemorySystemApp.Models.Identity;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationSettings applicationSettings;

        public IdentityService(
            UserManager<User> userManager,
            IOptions<ApplicationSettings> options)
        {
            this.userManager = userManager;
            this.applicationSettings = options.Value;
        }

        public async Task<Result<string>> Login(LoginUserRequestModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = await this.userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Result<string>.Failure(new[] { "Username or password are invalid" });
            }

            var validationResult = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!validationResult)
            {
                return Result<string>.Failure(new[] { "Username or password are invalid" });
            }

            return Result<string>.SuccessWith(this.GenerateJwtToken(user));
        }

        public async Task<Result<User>> Register(RegisterUserRequestModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException(nameof(model));
            }

            var user = AutoMapperConfig.MapperInstance.Map<User>(model);

            var identityResult = await this.userManager.CreateAsync(user, model.Password);

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? Result<User>.SuccessWith(user)
                : Result<User>.Failure(errors);
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

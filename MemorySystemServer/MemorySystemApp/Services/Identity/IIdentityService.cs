namespace MemorySystemApp.Services.Identity
{
    using System.Threading.Tasks;

    using MemorySystemApp.Data.Models;
    using MemorySystemApp.Models.Identity;

    public interface IIdentityService
    {
        public Task<Result<User>> Register(RegisterUserRequestModel model);

        public Task<Result<LoginModel>> Login(LoginUserRequestModel model);
    }
}

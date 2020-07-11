namespace MemorySystemApp.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    using MemorySystem.Infrastructure.AutomapperSettings;

    using MemorySystemApp.Data.Models;

    public class RegisterUserRequestModel : IMapTo<User>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

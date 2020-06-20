namespace MemorySystemApp.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Comments = new HashSet<Comment>();
            this.Pictures = new HashSet<Picture>();
        }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Picture> Pictures { get; set; }
    }
}

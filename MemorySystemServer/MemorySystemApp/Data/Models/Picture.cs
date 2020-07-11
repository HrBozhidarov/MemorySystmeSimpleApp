namespace MemorySystemApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        public Picture()
        {
            this.CategoryPictures = new HashSet<CategoryPicture>();
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
        }

        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public string Description { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public IEnumerable<CategoryPicture> CategoryPictures { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Like> Likes { get; set; }
    }
}

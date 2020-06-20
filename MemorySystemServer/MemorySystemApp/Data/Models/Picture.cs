namespace MemorySystemApp.Data.Models
{
    using System.Collections.Generic;

    public class Picture
    {
        public Picture()
        {
            this.CategoryPictures = new HashSet<CategoryPicture>();
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public string Url { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public IEnumerable<CategoryPicture> CategoryPictures { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}

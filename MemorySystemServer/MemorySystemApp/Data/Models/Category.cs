namespace MemorySystemApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.CategoryPictures = new HashSet<CategoryPicture>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<CategoryPicture> CategoryPictures { get; set; }
    }
}

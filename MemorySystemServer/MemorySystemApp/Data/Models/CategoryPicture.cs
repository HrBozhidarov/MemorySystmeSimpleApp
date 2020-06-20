namespace MemorySystemApp.Data.Models
{
    public class CategoryPicture
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int PictureId { get; set; }

        public Picture Picture { get; set; }
    }
}

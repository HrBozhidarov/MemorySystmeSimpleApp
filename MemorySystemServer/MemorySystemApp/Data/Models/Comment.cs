namespace MemorySystemApp.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PictureId { get; set; }

        public Picture Picture { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace telehealth.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        public int AuthorId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PostDate { get; set; } = DateTime.Now;

        public ICollection<Comment> Comments { get; set; }

    }
}

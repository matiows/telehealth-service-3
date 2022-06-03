using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace telehealth.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int BlogId { get; set; } = 0;

        public int HelpId { get; set; } = 0;

        public int CommentorId { get; set; }

        public string Body { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.Now;

    }
}

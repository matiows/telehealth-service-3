using System.ComponentModel.DataAnnotations;

namespace telehealth.Models
{
    public class Help
    {
        [Key]
        public int HelpId { get; set; }

        public int RequestorId { get; set; }

        public string Body { get; set; }

        public int Status { get; set; } = HELPSTATUS.REQUESTED;

        public DateTime PostDate { get; set; } = DateTime.Now;

        public ICollection<Comment> Comments { get; set; }
    }
}

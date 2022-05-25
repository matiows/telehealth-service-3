using System.ComponentModel.DataAnnotations;

namespace telehealth.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

    }
}

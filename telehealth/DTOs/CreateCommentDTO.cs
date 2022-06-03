namespace telehealth.DTOs
{
    public class CreateCommentDTO
    {
        public int BlogId { get; set; } = 0;

        public int HelpId { get; set; } = 0;

        public int CommentorId { get; set; }

        public string Body { get; set; }
    }
}

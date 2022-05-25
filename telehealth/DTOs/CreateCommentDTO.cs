namespace telehealth.DTOs
{
    public class CreateCommentDTO
    {
        public int BlogId { get; set; }

        public int CommentorId { get; set; }

        public string Body { get; set; }
    }
}

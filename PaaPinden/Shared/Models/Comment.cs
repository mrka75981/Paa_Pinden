namespace Shared.Models
{
    public class Comment
    {
        public int CommentKnittingPostId { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public string Writer { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}

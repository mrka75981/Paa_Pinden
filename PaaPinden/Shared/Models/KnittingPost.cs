namespace Shared.Models
{
    public class KnittingPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserId { get; set; }
        public List<Comment>? Comments { get; set; }
        public Content KnittingPostContent { get; set; }
    }
}

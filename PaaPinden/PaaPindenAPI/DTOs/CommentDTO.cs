using PaaPindenAPI.Models;

namespace PaaPindenAPI.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Writer { get; set; }
        public int CommentKnittingPostId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

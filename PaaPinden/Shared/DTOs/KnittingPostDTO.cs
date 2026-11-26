using Shared.Models;

namespace Shared.DTOs
{
    public class KnittingPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserId { get; set; }
        public Content KnittingPostContent { get; set; }
    }
}

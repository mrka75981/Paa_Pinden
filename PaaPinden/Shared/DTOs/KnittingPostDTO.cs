using Shared.Models;

namespace Shared.DTOs
{
    public class KnittingPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Author { get; set; }
        public Content KnittingPostContent { get; set; }
    }
}

namespace PaaPindenAPI.Models
{
    public class Content
    {
        public int KnittingPostId { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public List<ContentPicture> Pictures { get; set; }
        public List<ContentVideo> Videos { get; set; }
    }
}

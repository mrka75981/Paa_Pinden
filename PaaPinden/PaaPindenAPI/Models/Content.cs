namespace PaaPindenAPI.Models
{
    public class Content
    {
        private KnittingPost ContentKnittingPost { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public List<ContentPicture> Pictures { get; set; }
        public List<ContentVideo> Videos { get; set; }
    }
}

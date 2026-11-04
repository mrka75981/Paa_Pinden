using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private static List<Comment> comments = new List<Comment>
        {
            new Comment
            {
                CommentKnittingPost = new KnittingPost(),
                Id = 1,
                Text = "Første kommentar",
                Writer = "Anne jensen jørgensen",
                TimeStamp = DateTime.Today
            },

            new Comment
            {
                CommentKnittingPost = new KnittingPost(),
                Id = 2,
                Text = "Grim strik",
                Writer = "Hanne jensen jørgensen",
                TimeStamp = DateTime.Today
            }
        };

        public void CreateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAllCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetComments()
        {
            return comments;
        }

        public Comment UpdateCommentAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

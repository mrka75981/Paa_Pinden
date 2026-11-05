using PaaPindenAPI.Data;
using PaaPindenAPI.Models;

namespace PaaPindenAPI.Repositories
{
    public class KnittingPostRepository : IKnittingPostRepository
    {
        private readonly PaaPindenContext _paaPindenContext;

        public KnittingPostRepository(PaaPindenContext paaPindenContext)
        {
            _paaPindenContext = paaPindenContext;
        }

        public Task CreateKnittingPostAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteKnittingPostAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<KnittingPost>> GetAllKnittingPostAsync()
        {
            throw new NotImplementedException();
        }

        public Task<KnittingPost> GetKnittingPostAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateKnittingPostAsync(KnittingPost knittingPost)
        {
            throw new NotImplementedException();
        }
    }
}

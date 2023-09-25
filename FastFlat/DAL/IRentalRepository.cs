using FastFlat.Models;
namespace FastFlat.DAL
{
    public interface IRentalRepository
    {
        Task<IEnumerable<ListningModel>> getAll();
    }
}

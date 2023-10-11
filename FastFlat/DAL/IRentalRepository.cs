using FastFlat.Models;
using System.Threading.Tasks;

namespace FastFlat.DAL
{
    public interface IRentalRepository<T> where T : class
    {
        //må gjøre sånn for å få flere SQL spørringer. Får å få .include()
        IQueryable<T> GetAll();

        /* Brukes ikke
        Task<IEnumerable<T>> GetAllById(string Id);
        */

        Task<T> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task<bool> Delete(int id);
    }
}

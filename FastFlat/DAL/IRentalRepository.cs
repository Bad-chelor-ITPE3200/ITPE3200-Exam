using FastFlat.Models;

namespace FastFlat.DAL
{
    public interface IRentalRepository<T> where T : class
    {
        //må gjøre sånn for å få flere SQL spørringer. Får å få .include()
        IQueryable<T> GetAll();

        Task<T> GetById(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete( int id);

        /*

        Task<List<DateTime>> GetBookedDatesForListning(int listningId);

        Task<(DateTime? StartDate, DateTime? EndDate)> GetAvailableDatesForListning(int listningId);
        */

    }
}

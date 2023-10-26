using FastFlat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.DAL
{
    public class RentalRepository<T> : IRentalRepository<T> where T : class
    {
        private readonly RentalDbContext _context;
        private readonly DbSet<T> _dbSet;

        public RentalRepository(RentalDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<DateTime>> GetBookedDatesForListning(int listningId)
        {
            // Trinn 1: Hent alle bookings fra databasen
            var bookings = await _context.Bookings
                                         .Where(b => b.ListningId == listningId)
                                         .ToListAsync();

            // Trinn 2: Generer alle bookede datoer fra bookings
            var bookedDates = new List<DateTime>();

            foreach (var booking in bookings)
            {
                for (var date = booking.FromDate; date <= booking.ToDate; date = date.AddDays(1))
                {
                    bookedDates.Add(date);
                }
            }

            return bookedDates;
        }

        [HttpGet]
        public async Task<(DateTime? StartDate, DateTime? EndDate)> GetAvailableDatesForListning(int listningId)
        {
            var listning = await _context.Rentals.FindAsync(listningId);

            if (listning == null)
                return (null, null); // eller håndter på passende måte

            return (listning.FromDate, listning.ToDate);
        }

    }


}

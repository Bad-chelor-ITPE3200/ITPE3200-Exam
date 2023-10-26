using FastFlat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.DAL
{
    public class RentalRepository<T> : IRentalRepository<T> where T : class
    {
        private readonly RentalDbContext _context;
        private readonly DbSet<T> _dbSet;
        private ILogger<RentalRepository<T>> _logger; 

        public RentalRepository(RentalDbContext context, ILogger<RentalRepository<T>>logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger; 
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                _logger.LogInformation("Data is OK");
                return _context.Set<T>();
            }
            catch (Exception e)
            {
              _logger.LogCritical("error in getall in generic repository, error " +e);
                return null; 
            }
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
            // Step 1 get all the bookings from the database
            var bookings = await _context.Bookings
                                         .Where(b => b.ListningId == listningId)
                                         .ToListAsync();
            
            // Step 2: Genereate all booked dates
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
        [HttpGet("api/available-countries")]
        public async Task<List<string?>> GetAvailableCountries()
        {
            return await _context.Countries.Select(c => c.Contryname).ToListAsync(); 
        }

        [HttpGet]
        public async Task<(DateTime? StartDate, DateTime? EndDate)> GetAvailableDatesForListning(int listningId)
        {
            var listning = await _context.Rentals.FindAsync(listningId);

            if (listning == null)
                return (null, null); // handels exeptions

            return (listning.FromDate, listning.ToDate);
        }

    }


}

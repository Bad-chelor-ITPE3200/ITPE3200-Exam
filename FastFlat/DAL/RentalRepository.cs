using Castle.Core.Logging;
using FastFlat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.DAL
{
    public class RentalRepository<T> : IRentalRepository<T> where T : class
    {
        private readonly RentalDbContext _context;
        private readonly DbSet<T> _dbSet;

        private readonly ILogger<RentalRepository<T>> _logger;

        public RentalRepository(RentalDbContext context, ILogger<RentalRepository<T>> logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
        }

        public IQueryable<T> GetAll() //hvorfor har vi ikke async her?
        {
            try
            {
                return _context.Set<T>();
            }

            catch(Exception e)
            {
                _logger.LogError("[RentalRepository] Set<T> failed when GetAll(), error message: {e}", e.Message);
                return null;       
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("[RentalRepository] <T> FindAsync(id) failed when GetById for id {id:0000}, error message: {e}", id, e.Message);
                return null;
            }  
        }

        public async Task<bool> Create(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[RentalRepository] entity creation failed for entinty {@entity}, error message: {e}", entity, e.Message);
                return false;
            }
            
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[RentalRepositry] entity Update(entity) failed when updating the entityID {EntityId:0000}, error message: {e}", entity, e.Message);
                return false;
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                {
                    _logger.LogError("[RentalRepository] entity not found for the entityId {EntityId:0000}", id);
                    return false;
                }
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                _logger.LogError("[RentalRepository] entity deletion failed for entityId {ItemId:0000}, error message: {e}", id, e.Message);
                return false;
            }
          
            
        }

        /*

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
        */

        [HttpGet("api/available-countries")]
        public async Task<List<string?>> GetAvailableCountries()
        {
            return await _context.Countries.Select(c => c.Contryname).ToListAsync();
        }


    }


}

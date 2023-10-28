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

        //prøver å gjøre det til async
        /*
        public IQueryable<T> GetAll() //hvorfor har vi ikke async her?
        {
            try
            {
                return _context.Set<T>();
            }

            catch (Exception e)
            {
                _logger.LogError("[RentalRepository] Set<T> failed when GetAll(), error message: {e}", e.Message);
                return null;
            }
        }
        */

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                // Returner en liste for � utf�re en umiddelbar evaluering, akkurat som i l�rerens eksempel.
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception e)
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

    }
}

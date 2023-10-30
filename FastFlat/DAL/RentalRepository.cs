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

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                //returns a list with all the context of the tables
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("[RentalRepository] Set<T> failed when GetAll(), error message: {e}", e.Message);
                return null!;
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                //finds an item by id, by using the entityframework
                return (await _dbSet.FindAsync(id))!;
            }
            catch (Exception e)
            {
                _logger.LogError("[RentalRepository] <T> FindAsync(id) failed when GetById for id {id:0000}, error message: {e}", id, e.Message);
                return null!;
            }
        }

        public async Task<bool> Create(T entity)
        {
            try
            {
                //creates a new entity in the database. 
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
                //updates the enity
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
                var entity = await _dbSet.FindAsync(id); //finds the thing that needs to be deleted
                if (entity == null)
                {
                    _logger.LogError("[RentalRepository] entity not found for the entityId {EntityId:0000}", id);
                    return false;
                }
                _dbSet.Remove(entity); //removes the entity
                await _context.SaveChangesAsync(); //saves the changes
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

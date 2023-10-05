using Microsoft.EntityFrameworkCore;
using FastFlat.Models;

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

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllById(string Id)
        {
            var allEntities = await _dbSet.ToListAsync();
            return allEntities
                            .Where(entity => typeof(T).GetProperty("UserId") != null &&
                                             typeof(T).GetProperty("UserId").PropertyType == typeof(string) &&
                                             entity.GetType().GetProperty("UserId").GetValue(entity).ToString() == Id).ToList();


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
    }
}

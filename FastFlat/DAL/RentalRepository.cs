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
        // Method to retrieve a listing (in this case listning model) tied to a specific session
        public async Task<IEnumerable<T>> GetAllById(string Id)
        {
            var allEntities = await _dbSet.ToListAsync();
            return allEntities  // Returns all instances of the model table in question and filters it if the provided id matches on of the ids in the table
                            .Where(entity => typeof(T).GetProperty("UserId") != null && // Checks if the class T has a UserId that is not null AND
                                             typeof(T).GetProperty("UserId").PropertyType == typeof(string) &&  // Checks that the UserID is a String AND
                                             entity.GetType().GetProperty("UserId").GetValue(entity).ToString() == Id).ToList();
                                             // Above extracts the UserID property value from the Table that is called and sees if 
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

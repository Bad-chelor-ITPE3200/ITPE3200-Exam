using Microsoft.EntityFrameworkCore;
using FastFlat.Models;

namespace FastFlat.DAL
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentalDbContext _db;

        public RentalRepository(RentalDbContext db)
        {
              _db = db;
        }
        public async Task<IEnumerable<ListningModel>> getAll()
        {
            return await _db.Rentals.ToListAsync();
        }
    }
}

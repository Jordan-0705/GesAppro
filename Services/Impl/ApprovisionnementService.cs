using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Impl
{
    public class ApprovisionnementService : IApprovisionnementService
    {
        private readonly GesApproDbContext _context;

        public ApprovisionnementService(GesApproDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Approvisionnement>> GetAllAsync()
        {
            return await _context.Approvisionnements!
                .Include(a => a.Fournisseur)
                .Include(a => a.Article)
                .OrderByDescending(a => a.DateAppro)
                .ToListAsync();
        }

        public async Task<Approvisionnement?> GetByIdAsync(int id)
        {
            return await _context.Approvisionnements!
                .Include(a => a.Fournisseur)
                .Include(a => a.Article)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Approvisionnement> CreateAsync(Approvisionnement appro)
        {
            _context.Approvisionnements!.Add(appro);
            await _context.SaveChangesAsync();
            return appro;
        }

        public async Task<Approvisionnement> UpdateAsync(Approvisionnement appro)
        {
            _context.Approvisionnements!.Update(appro);
            await _context.SaveChangesAsync();
            return appro;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appro = await _context.Approvisionnements!.FindAsync(id);
            if (appro == null)
                return false;

            _context.Approvisionnements.Remove(appro);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<Approvisionnement>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Approvisionnements.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(a => a.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Approvisionnement>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
    

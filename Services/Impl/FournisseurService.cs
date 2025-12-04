using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Impl
{
    public class FournisseurService : IFournisseurService
    {
        private readonly GesApproDbContext _context;

        public FournisseurService(GesApproDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fournisseur>> GetAllAsync()
        {
            return await _context.Fournisseurs!
                .Include(f => f.Approvisionnements)
                .OrderByDescending(f => f.DateCreation)
                .ToListAsync();
        }

        public async Task<Fournisseur?> GetByIdAsync(int id)
        {
            return await _context.Fournisseurs!.FindAsync(id);
        }

        public async Task<Fournisseur> CreateAsync(Fournisseur fournisseur)
        {
            _context.Fournisseurs!.Add(fournisseur);
            await _context.SaveChangesAsync();
            return fournisseur;
        }

        public async Task<Fournisseur> UpdateAsync(Fournisseur fournisseur)
        {
            _context.Fournisseurs!.Update(fournisseur);
            await _context.SaveChangesAsync();
            return fournisseur;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fournisseur = await _context.Fournisseurs!.FindAsync(id);
            if (fournisseur == null)
                return false;

            _context.Fournisseurs.Remove(fournisseur);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

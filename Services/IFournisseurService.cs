using Models;

namespace Services
{
    public interface IFournisseurService
    {
        Task<IEnumerable<Fournisseur>> GetAllAsync();
        Task<Fournisseur?> GetByIdAsync(int id);
        Task<Fournisseur> CreateAsync(Fournisseur fournisseur);
        Task<Fournisseur> UpdateAsync(Fournisseur fournisseur);
        Task<bool> DeleteAsync(int id);
    }
}

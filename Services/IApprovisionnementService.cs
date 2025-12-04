using Models;

namespace Services
{
    public interface IApprovisionnementService
    {
        Task<IEnumerable<Approvisionnement>> GetAllAsync();
        Task<Approvisionnement?> GetByIdAsync(int id);
        Task<Approvisionnement> CreateAsync(Approvisionnement appro);
        Task<Approvisionnement> UpdateAsync(Approvisionnement appro);
        Task<bool> DeleteAsync(int id);
        Task<PagedResult<Approvisionnement>> GetPagedAsync(int page, int pageSize);
    }
}

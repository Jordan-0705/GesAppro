using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Impl
{
    public class ArticleService : IArticleService
    {
        private readonly GesApproDbContext _context;

        public ArticleService(GesApproDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles!
                .OrderBy(a => a.Libelle)
                .ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Articles!.FindAsync(id);
        }

        public async Task<Article> CreateAsync(Article article)
        {
            _context.Articles!.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<Article> UpdateAsync(Article article)
        {
            _context.Articles!.Update(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var article = await _context.Articles!.FindAsync(id);
            if (article == null)
                return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

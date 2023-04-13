using CondominioAPI.Domain.Entities;
using CondominioAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CondominioAPI.Infrastructure.Repositories
{
    public class CondominioRepository : ICondominioRepository
    {
        private readonly AppDbContext _context;

        public CondominioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Condominio>> GetAllAsync()
        {
            return await _context.Condominios.ToListAsync();
        }

        public async Task<Condominio?> GetByIdAsync(Guid id)
        {
            return await _context.Condominios.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Condominio> AddAsync(Condominio condominio)
        {
            await _context.Condominios.AddAsync(condominio);
            await _context.SaveChangesAsync();
            return condominio;
        }

        public async Task UpdateAsync(Condominio condominioToUpdate)
        {
            _context.Condominios.Update(condominioToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var condominio = await _context.Condominios.FirstOrDefaultAsync(c => c.Id == id);
            if (condominio != null)
            {
                _context.Condominios.Remove(condominio);
                await _context.SaveChangesAsync();
            }
        }
    }
}

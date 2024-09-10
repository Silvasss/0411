using _0411.Contracts;
using _0411.Data;
using _0411.Model;
using Microsoft.EntityFrameworkCore;

namespace _0411.Repositories
{
    public class QuartoRepository(DataContext dataContext) : GenericoRepository<Quarto>(dataContext), IQuartoRepository
    {
        public async Task<object> Get1(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _entityFramework.Set<Quarto>()
                .AsNoTracking()
                .Include(q => q.TipoQuarto)
                .Select(q => new
                {
                    q.Id,
                    q.Numero_Quarto,
                    q.Status,
                    q.UpdatedAt,
                    TipoQuarto = new
                    {
                        q.TipoQuarto.Descricao,
                        q.TipoQuarto.Valor_Diaria
                    }
                })
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<object> GetAll2()
        {
            return await _entityFramework.Set<Quarto>()
                .Select(x => new 
                { 
                    x.Id,
                    x.Numero_Quarto,
                    x.Status
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

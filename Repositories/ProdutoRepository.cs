using _0411.Contracts;
using _0411.Data;
using _0411.Model;
using Microsoft.EntityFrameworkCore;

namespace _0411.Repositories
{
    public class ProdutoRepository(DataContext dataContext) : GenericoRepository<Produto>(dataContext), IProdutoRepository
    {
        public async Task<object> Get1(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _entityFramework.Set<Produto>()
                .AsNoTracking()
                .Select(q => new
                {
                    q.Id,
                    q.Nome,
                    q.Valor
                })
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<object> GetAll2()
        {
            return await _entityFramework.Set<Produto>()
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.Valor
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

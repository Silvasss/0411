using _0411.Contracts;
using _0411.Data;
using _0411.Model;
using Microsoft.EntityFrameworkCore;

namespace _0411.Repositories
{
    public class ConsumoRepository(DataContext dataContext) : GenericoRepository<Consumo>(dataContext), IConsumoRepository
    {
        public async Task<object> Get1(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _entityFramework.Set<Consumo>()
                .AsNoTracking()
                .Include(c => c.Produto)
                .Include(c => c.Reserva)
                .Select(x => new
                {
                    x.Id,
                    x.Quantidade,
                    valorTota = x.Quantidade * x.Produto.Valor,
                    ultimaData = x.UpdatedAt ?? x.CreatedAt, 
                    x.Reserva.Numero_Quarto,
                    Produto = new
                    {
                        x.Produto.Nome,
                        x.Produto.Valor
                    }
                })
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<object> GetAll2()
        {
            return await _entityFramework.Set<Consumo>()
                .Include(c => c.Produto)
                .Include(c => c.Reserva)
                .Select(x => new
                {
                    x.Id,
                    x.Quantidade,
                    x.CreatedAt,
                    x.Produto.Nome,
                    x.Produto.Valor,
                    x.Reserva.Numero_Quarto
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

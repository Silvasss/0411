using _0411.Contracts;
using _0411.Data;
using _0411.Model;
using Microsoft.EntityFrameworkCore;

namespace _0411.Repositories
{
    public class PagamentoRepository(DataContext dataContext) : GenericoRepository<Pagamento>(dataContext), IPagamentoRepository
    {
        public async Task<object> Get1(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _entityFramework.Set<Pagamento>()
                .AsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Status,
                    x.Valor_Pago,
                    x.Valor_Total,
                    x.Forma_Pagamento,
                    x.Observacoes,
                    x.Data_Pagamento,
                    x.CreatedAt,
                    x.UpdatedAt
                })
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<object> GetAll2()
        {
            return await _entityFramework.Set<Pagamento>()
                .Select(x => new
                {
                    x.Id,
                    x.Status,
                    x.Valor_Pago,
                    x.Valor_Total,
                    x.CreatedAt
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

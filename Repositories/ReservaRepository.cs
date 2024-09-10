using _0411.Contracts;
using _0411.Data;
using _0411.Model;
using Microsoft.EntityFrameworkCore;

namespace _0411.Repositories
{
    public class ReservaRepository(DataContext dataContext) : GenericoRepository<Reserva>(dataContext), IReservaRepository
    {
        public async Task<object> Get1(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _entityFramework.Set<Reserva>()
                .AsNoTracking()
                .Include(c => c.Cliente)
                .Include(c => c.Pagamentos)
                .Include(c => c.Consumos)
                .Select(x => new
                {
                    x.Id,
                    x.Status,
                    x.Quantidade_Dias,
                    x.Numero_Quarto,
                    x.Data_Checkin,
                    x.Data_Checkout,
                    x.CreatedAt,
                    x.UpdatedAt,
                    Cliente = new 
                    { 
                        x.Cliente_Id,
                        x.Cliente.Nome
                    },
                    Consumo = x.Consumos.Select(e => new
                    {
                        e.Id,
                        e.Quantidade,
                        e.Produto.Nome,
                        e.Produto.Valor
                    }),
                    Pagamento = x.Pagamentos.Select(e => new
                    {
                        e.Id,
                        e.Valor_Pago,
                        e.Valor_Total,
                        e.Forma_Pagamento,
                        e.Status,
                        e.Observacoes,
                        e.Data_Pagamento
                    })
                })
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<object> GetAll2()
        {
            return await _entityFramework.Set<Reserva>()
                .Select(x => new
                {
                    x.Id,
                    x.Status,
                    x.Quantidade_Dias,
                    x.Numero_Quarto,
                    x.Data_Checkin,
                    x.Data_Checkout,
                    x.CreatedAt,
                    x.UpdatedAt
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

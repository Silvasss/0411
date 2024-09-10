using _0411.Contracts;
using _0411.Data;
using _0411.Model;
using Microsoft.EntityFrameworkCore;

namespace _0411.Repositories
{
    public class TipoQuartoRepository(DataContext dataContext) : GenericoRepository<Tipo_Quarto>(dataContext), ITipoQuartoRepository
    {
        public async Task<IEnumerable<object>> GetSelectAll()
        {
            return await _entityFramework.Set<Tipo_Quarto>().Select(x => new { x.Id, x.Descricao, x.Valor_Diaria }).AsNoTracking().ToListAsync();
        }
    }
}

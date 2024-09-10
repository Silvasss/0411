using _0411.Model;

namespace _0411.Contracts
{
    public interface ITipoQuartoRepository : IGenericoRepository<Tipo_Quarto>
    {
        Task<IEnumerable<object>> GetSelectAll();
    }
}

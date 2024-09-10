using _0411.Model;

namespace _0411.Contracts
{
    public interface IQuartoRepository : IGenericoRepository<Quarto>
    {
        Task<object> Get1(int id);
        Task<object> GetAll2();
    }
}

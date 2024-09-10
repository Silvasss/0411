using _0411.Model;

namespace _0411.Contracts
{
    public interface IProdutoRepository : IGenericoRepository<Produto>
    {
        Task<object> Get1(int id);
        Task<object> GetAll2();
    }
}

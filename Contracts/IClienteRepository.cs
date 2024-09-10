using _0411.Dtos;
using _0411.Model;

namespace _0411.Contracts
{
    public interface IClienteRepository : IGenericoRepository<Cliente>
    {
        Task<object> Get1(int id);
        Task<object> GetAll2();
        Task<bool> Post(int id, ClienteDto dto);
        Task<bool> Put(int id, ClienteDto dto);
    }
}

using _0411.Dtos;
using _0411.Model;

namespace _0411.Contracts
{
    public interface IUsuarioRepository : IGenericoRepository<Usuario>
    {
        Task<object> Get1(int id);
        Task<bool> Put(int id, UsuarioDto dto);
    }
}

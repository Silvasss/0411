using _0411.Contracts;
using _0411.Data;
using _0411.Dtos;
using _0411.Model;
using Microsoft.EntityFrameworkCore;

namespace _0411.Repositories
{
    public class UsuarioRepository(DataContext dataContext) : GenericoRepository<Usuario>(dataContext), IUsuarioRepository
    {
        public async Task<object> Get1(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _entityFramework.Set<Usuario>()
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Include(e => e.InformacoesEmprego)
                .Include(e => e.InformacoesUsuario)
                .Select(x => new
                {
                    x.Nome,
                    x.InformacoesEmprego,
                    x.InformacoesUsuario
                })
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<bool> Put(int id, UsuarioDto dto)
        {
            Usuario? db = await _entityFramework.Set<Usuario>()
                .Where(x => x.Id == id)
                .Include(e => e.InformacoesEmprego)
                .Include(e => e.InformacoesUsuario)
                .FirstOrDefaultAsync();

            db.Nome = dto.Nome;
            db.InformacoesEmprego.Data_Admissao = dto.InformacoesEmprego.Data_Admissao;
            db.InformacoesEmprego.Salario = dto.InformacoesEmprego.Salario;
            db.InformacoesEmprego.Status = dto.InformacoesEmprego.Status;
            db.InformacoesUsuario.Telefone = dto.InformacoesUsuario.Telefone;
            db.InformacoesUsuario.Rg = dto.InformacoesUsuario.Rg;
            db.InformacoesUsuario.Cpf = dto.InformacoesUsuario.Cpf;
            db.InformacoesUsuario.Endereco = dto.InformacoesUsuario.Endereco;
            db.InformacoesUsuario.Cidade = dto.InformacoesUsuario.Cidade;
            db.InformacoesUsuario.Estado = dto.InformacoesUsuario.Estado;
            db.InformacoesUsuario.Pais = dto.InformacoesUsuario.Pais;

            if (await _entityFramework.SaveChangesAsync() > 0) { return true; }

            return false;
        }
    }
}

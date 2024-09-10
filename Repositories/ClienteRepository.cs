using _0411.Contracts;
using _0411.Data;
using _0411.Dtos;
using _0411.Model;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace _0411.Repositories
{
    public class ClienteRepository(DataContext dataContext) : GenericoRepository<Cliente>(dataContext), IClienteRepository
    {
        public async Task<object> Get1(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _entityFramework.Set<Cliente>()
                .AsNoTracking()
                .Include(e => e.Usuario)
                .Include(e => e.InformacoesCliente)
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.CreatedAt,
                    funcionario = x.Usuario.Nome,
                    informacoesCliente = new InformacoesClienteDto()
                    {
                        Telefone = x.InformacoesCliente.Telefone,
                        Rg = x.InformacoesCliente.Rg,
                        Cpf = x.InformacoesCliente.Cpf,
                        Endereco = x.InformacoesCliente.Endereco,
                        Cidade = x.InformacoesCliente.Cidade,
                        Estado = x.InformacoesCliente.Estado,
                        Pais = x.InformacoesCliente.Pais
                    }
                })
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<object> GetAll2()
        {
            return await _entityFramework.Set<Cliente>()
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.CreatedAt
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Post(int id, ClienteDto dto)
        {
            //Cliente temp = new()
            //{
            //    Nome = dto.Nome,
            //    Usuario_Id = id,
            //    Id_UltimoUsuario = id,
            //    InformacoesCliente = new Informacoes_Cliente()
            //    {
            //        Telefone = dto.InformacoesCliente.Telefone,
            //        Rg = dto.InformacoesCliente.Rg,
            //        Cpf = dto.InformacoesCliente.Cpf,
            //        Endereco = dto.InformacoesCliente.Endereco,
            //        Cidade = dto.InformacoesCliente.Cidade,
            //        Estado = dto.InformacoesCliente.Estado,
            //        Pais = dto.InformacoesCliente.Pais
            //    },
            //    Reservas = dto.Reservas.Select(x => new Reserva()
            //    {
            //        Quantidade_Dias = x.Quantidade_Dias,
            //        Status = x.Status,
            //        Numero_Quarto = x.Numero_Quarto,
            //        Data_Checkin = x.Data_Checkin,
            //        Data_Checkout = x.Data_Checkout,
            //        Pagamentos = x.Pagamento.Select(e => new Pagamento()
            //        {
            //            Valor_Pago = e.Valor_Pago,
            //            Valor_Total = e.Valor_Total,
            //            Forma_Pagamento = e.Forma_Pagamento,
            //            Status = e.Status,
            //            Observacoes = e.Observacoes,
            //            Data_Pagamento = e.Data_Pagamento
            //        }).ToList(),
            //        Consumos = x.Consumos.Select(e => new Consumo()
            //        {
            //            Quantidade = e.Quantidade,
            //            Produto_Id = e.Produto_Id
            //        }).ToList()
            //    }).ToList()
            //};

            var fake = new Faker();

            // Gera a primeira data
            DateTime startDate = fake.Date.Between(new DateTime(2017, 10, 1), DateTime.Now);

            // Gera a segunda data, garantindo que seja maior ou igual à primeira data
            DateTime endDate = fake.Date.Between(startDate, DateTime.Now);

            Cliente temp = new()
            {
                Nome = fake.Name.FirstName(),
                Usuario_Id = id,
                Id_UltimoUsuario = id,
                InformacoesCliente = new Informacoes_Cliente()
                {
                    Telefone = fake.Random.ReplaceNumbers("############"),
                    Rg = fake.Random.AlphaNumeric(11),
                    Cpf = fake.Random.AlphaNumeric(11),
                    Endereco = fake.Address.Direction(),
                    Cidade = fake.Address.City(),
                    Estado = fake.Address.State(),
                    Pais = fake.Address.Country()
                },
                Reservas = dto.Reservas.Select(x => new Reserva()
                {
                    Quantidade_Dias = fake.Random.Number(1, 30),
                    Status = fake.Random.ListItem(["confirmada", "ocupado", "cancelada"]),
                    Numero_Quarto = fake.Random.Number(1, 9),
                    Data_Checkin = DateTime.SpecifyKind(startDate, DateTimeKind.Utc),
                    Data_Checkout = DateTime.SpecifyKind(endDate, DateTimeKind.Utc),
                    Pagamentos = x.Pagamento.Select(e => new Pagamento()
                    {
                        Valor_Pago = e.Valor_Pago,
                        Valor_Total = e.Valor_Total,
                        Forma_Pagamento = e.Forma_Pagamento,
                        Status = e.Status,
                        Observacoes = e.Observacoes,
                        Data_Pagamento = e.Data_Pagamento
                    }).ToList(),
                    Consumos = x.Consumos.Select(e => new Consumo()
                    {
                        Quantidade = fake.Random.Number(1, 30),
                        Produto_Id = fake.Random.Number(1, 2)
                    }).ToList()
                }).ToList()
            };

            await _entityFramework.AddAsync(temp);

            if (await _entityFramework.SaveChangesAsync() > 0) { return true; };

            return true;
        }

        public async Task<bool> Put(int id, ClienteDto dto)
        {
            Cliente? db = await _entityFramework.Set<Cliente>()
                .Where(x => x.Id == dto.Id)
                .Include(e => e.InformacoesCliente)
                .FirstOrDefaultAsync();

            if (db == null) { return false; }

            db.Nome = dto.Nome;
            db.Id_UltimoUsuario = id;
            db.InformacoesCliente.Telefone = dto.InformacoesCliente.Telefone;
            db.InformacoesCliente.Rg = dto.InformacoesCliente.Rg;
            db.InformacoesCliente.Cpf = dto.InformacoesCliente.Cpf;
            db.InformacoesCliente.Endereco = dto.InformacoesCliente.Endereco;
            db.InformacoesCliente.Cidade = dto.InformacoesCliente.Cidade;
            db.InformacoesCliente.Estado = dto.InformacoesCliente.Estado;
            db.InformacoesCliente.Pais = dto.InformacoesCliente.Pais;

            await _entityFramework.SaveChangesAsync();

            return true;
        }
    }
}

using _0411.Model;
using Microsoft.EntityFrameworkCore;

namespace _0411.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        DbSet<Auth> Auth { get; set; }
        DbSet<TipoConta> TipoContas { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Informacoes_Emprego> Informacoes_Emprego { get; set; }
        DbSet<Tipo_Quarto> Tipo_Quartos { get; set; }
        DbSet<Quarto> Quartos { get; set; }
        DbSet<Produto> Produtos { get; set; }
        DbSet<Informacoes_Usuario> Informacoes_Usuario { get; set; }
        DbSet<Cliente> Clientes { get; set; }
        DbSet<Reserva> Reservas { get; set; }
        DbSet<Pagamento> Pagamentos { get; set; }
        DbSet<Consumo> Consumos { get; set; }
        DbSet<Informacoes_Cliente> Informacoes_Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tipo de conta
            modelBuilder.Entity<TipoConta>().Property(t => t.Nome).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<TipoConta>().HasIndex(t => t.Nome).IsUnique(true);
            modelBuilder.Entity<TipoConta>().Property(t => t.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<TipoConta>().Property(t => t.UpdatedAt);
            modelBuilder.Entity<TipoConta>().HasMany(t => t.Auth).WithOne(t => t.TipoConta).HasForeignKey(t => t.TipoConta_Id).IsRequired();

            // Autenticação
            modelBuilder.Entity<Auth>().Property(a => a.User).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Auth>().HasIndex(a => a.User).IsUnique(true);
            modelBuilder.Entity<Auth>().Property(a => a.PasswordHash).IsRequired();
            modelBuilder.Entity<Auth>().Property(a => a.PasswordSalt).IsRequired();
            modelBuilder.Entity<Auth>().Property(a => a.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Auth>().Property(a => a.UpdatedAt);
            modelBuilder.Entity<Auth>().HasOne(a => a.Usuario).WithOne(a => a.Auth).HasForeignKey<Usuario>(a => a.Auth_Id).IsRequired();

            // Tipo de quarto
            modelBuilder.Entity<Tipo_Quarto>().Property(t => t.Descricao).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Tipo_Quarto>().Property(t => t.Valor_Diaria).HasPrecision(5, 2).IsRequired();
            modelBuilder.Entity<Tipo_Quarto>().Property(t => t.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Tipo_Quarto>().Property(t => t.UpdatedAt);
            modelBuilder.Entity<Tipo_Quarto>().HasMany(t => t.Quarto).WithOne(t => t.TipoQuarto).HasForeignKey(t => t.TipoQuarto_Id).IsRequired();

            // Quarto
            modelBuilder.Entity<Quarto>().Property(q => q.Numero_Quarto).HasMaxLength(1).IsRequired();
            modelBuilder.Entity<Quarto>().Property(q => q.Status).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Quarto>().Property(q => q.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Quarto>().Property(q => q.UpdatedAt);

            // Produto
            modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Valor).HasPrecision(5, 2).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Produto>().Property(p => p.UpdatedAt);
            modelBuilder.Entity<Produto>().HasMany(p => p.Consumos).WithOne(p => p.Produto).HasForeignKey(p => p.Produto_Id).IsRequired();

            // Consumo 
            modelBuilder.Entity<Consumo>().Property(c => c.Quantidade).HasMaxLength(2).IsRequired();
            modelBuilder.Entity<Consumo>().Property(c => c.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Consumo>().Property(c => c.UpdatedAt);

            // Reserva
            modelBuilder.Entity<Reserva>().Property(r => r.Status).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Reserva>().Property(r => r.Quantidade_Dias).HasMaxLength(2).IsRequired();
            modelBuilder.Entity<Reserva>().Property(r => r.Numero_Quarto).HasMaxLength(1).IsRequired();
            modelBuilder.Entity<Reserva>().Property(r => r.Data_Checkin);
            modelBuilder.Entity<Reserva>().Property(r => r.Data_Checkout);
            modelBuilder.Entity<Reserva>().Property(r => r.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Reserva>().Property(r => r.UpdatedAt);
            modelBuilder.Entity<Reserva>().HasMany(r => r.Consumos).WithOne(r => r.Reserva).HasForeignKey(r => r.Reserva_Id);
            modelBuilder.Entity<Reserva>().HasMany(r => r.Pagamentos).WithOne(r => r.Reserva).HasForeignKey(r => r.Reserva_Id);

            // Pagamento
            modelBuilder.Entity<Pagamento>().Property(p => p.Valor_Pago).HasPrecision(5, 2).IsRequired();
            modelBuilder.Entity<Pagamento>().Property(p => p.Valor_Total).HasPrecision(5, 2).IsRequired();
            modelBuilder.Entity<Pagamento>().Property(p => p.Forma_Pagamento).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Pagamento>().Property(p => p.Status).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<Pagamento>().Property(p => p.Observacoes).HasMaxLength(100);
            modelBuilder.Entity<Pagamento>().Property(p => p.Data_Pagamento);
            modelBuilder.Entity<Pagamento>().Property(p => p.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Pagamento>().Property(p => p.UpdatedAt);

            // Cliente
            modelBuilder.Entity<Cliente>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.Id_UltimoUsuario).IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Cliente>().Property(c => c.UpdatedAt);
            modelBuilder.Entity<Cliente>().HasMany(c => c.Reservas).WithOne(c => c.Cliente).HasForeignKey(c => c.Cliente_Id).IsRequired();
            modelBuilder.Entity<Cliente>().HasOne(c => c.InformacoesCliente).WithOne(c => c.Cliente).HasForeignKey<Informacoes_Cliente>(i => i.Cliente_Id).IsRequired();

            // Informações do cliente
            modelBuilder.Entity<Informacoes_Cliente>().Property(i => i.Telefone).HasMaxLength(13);
            modelBuilder.Entity<Informacoes_Cliente>().Property(i => i.Rg).HasMaxLength(14);
            modelBuilder.Entity<Informacoes_Cliente>().Property(i => i.Cpf).HasMaxLength(11);
            modelBuilder.Entity<Informacoes_Cliente>().Property(i => i.Endereco).HasMaxLength(100);
            modelBuilder.Entity<Informacoes_Cliente>().Property(i => i.Cidade).HasMaxLength(20);
            modelBuilder.Entity<Informacoes_Cliente>().Property(i => i.Estado).HasMaxLength(20);
            modelBuilder.Entity<Informacoes_Cliente>().Property(i => i.Pais).HasMaxLength(20);
            modelBuilder.Entity<Informacoes_Cliente>().Property(i => i.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Informacoes_Cliente>().Property(i => i.UpdatedAt);

            // Informações de emprego
            modelBuilder.Entity<Informacoes_Emprego>().Property(i => i.Data_Admissao).IsRequired();
            modelBuilder.Entity<Informacoes_Emprego>().Property(i => i.Salario).HasPrecision(7, 2).IsRequired();
            modelBuilder.Entity<Informacoes_Emprego>().Property(i => i.Status).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<Informacoes_Emprego>().Property(i => i.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Informacoes_Emprego>().Property(i => i.UpdatedAt);

            // Informações do usuário
            modelBuilder.Entity<Informacoes_Usuario>().Property(i => i.Telefone).HasMaxLength(13);
            modelBuilder.Entity<Informacoes_Usuario>().Property(i => i.Rg).HasMaxLength(14);
            modelBuilder.Entity<Informacoes_Usuario>().Property(i => i.Cpf).HasMaxLength(11);
            modelBuilder.Entity<Informacoes_Usuario>().Property(i => i.Endereco).HasMaxLength(100);
            modelBuilder.Entity<Informacoes_Usuario>().Property(i => i.Cidade).HasMaxLength(20);
            modelBuilder.Entity<Informacoes_Usuario>().Property(i => i.Estado).HasMaxLength(20);
            modelBuilder.Entity<Informacoes_Usuario>().Property(i => i.Pais).HasMaxLength(20);
            modelBuilder.Entity<Informacoes_Usuario>().Property(i => i.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Informacoes_Usuario>().Property(i => i.UpdatedAt);

            // Usuário (Funcionário)          
            modelBuilder.Entity<Usuario>().Property(u => u.Nome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Usuario>().Property(u => u.UpdatedAt);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Clientes).WithOne(c => c.Usuario).HasForeignKey(c => c.Usuario_Id).IsRequired();
            modelBuilder.Entity<Usuario>().HasOne(a => a.InformacoesEmprego).WithOne(a => a.Usuario).HasForeignKey<Informacoes_Emprego>(i => i.Usuario_Id);
            modelBuilder.Entity<Usuario>().HasOne(a => a.InformacoesUsuario).WithOne(a => a.Usuario).HasForeignKey<Informacoes_Usuario>(i => i.Usuario_Id);
        }
    }
}

namespace _0411.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int Id_UltimoUsuario { get; set; } // O id do último usuário que efetuou a alteração no registro
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Usuario? Usuario { get; set; }
        public int Usuario_Id { get; set; }
        public IEnumerable<Reserva>? Reservas { get; set; }
        public Informacoes_Cliente? InformacoesCliente { get; set; }
    }
    public partial class Reserva
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public int Quantidade_Dias { get; set; }
        public int Numero_Quarto { get; set; }
        public DateTime? Data_Checkin { get; set; }
        public DateTime? Data_Checkout { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Cliente? Cliente { get; set; }
        public int Cliente_Id { get; set; }
        public IEnumerable<Pagamento>? Pagamentos { get; set; }
        public IEnumerable<Consumo>? Consumos { get; set; }
    }
    public partial class Pagamento
    {
        public int Id { get; set; }
        public decimal Valor_Pago { get; set; }
        public decimal Valor_Total { get; set; }
        public string? Forma_Pagamento { get; set; }
        public string? Status { get; set; }
        public string? Observacoes { get; set; }
        public DateTime? Data_Pagamento { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Reserva? Reserva { get; set; }
        public int Reserva_Id { get; set; }
    }
    public partial class Consumo
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Produto? Produto { get; set; }
        public int Produto_Id { get; set; }
        public Reserva? Reserva { get; set; }
        public int Reserva_Id { get; set; }
    }
    public partial class Informacoes_Cliente
    {
        public int Id { get; set; }
        public string? Telefone { get; set; }
        public string? Rg { get; set; }
        public string? Cpf { get; set; }
        public string? Endereco { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; } 
        public string? Pais { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Cliente? Cliente { get; set; }
        public int Cliente_Id { get; set; }
    }
}

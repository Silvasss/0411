namespace _0411.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public InformacoesClienteDto? InformacoesCliente { get; set; }
        public IEnumerable<ReservaDto>? Reservas { get; set; }
    }
    public class ConsumoDto
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public int Produto_Id { get; set; }
        public int Reserva_Id { get; set; }
    }
    public class PagamentoDto
    {
        public int Id { get; set; }
        public decimal Valor_Pago { get; set; }
        public decimal Valor_Total { get; set; }
        public string? Forma_Pagamento { get; set; }
        public string? Status { get; set; }
        public string? Observacoes { get; set; }
        public DateTime? Data_Pagamento { get; set; }
        public int Reserva_Id { get; set; }
    }
    public class ReservaDto
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public int Quantidade_Dias { get; set; }
        public int Numero_Quarto { get; set; }
        public DateTime? Data_Checkin { get; set; }
        public DateTime? Data_Checkout { get; set; }
        public int Cliente_Id { get; set; }
        public IEnumerable<PagamentoDto>? Pagamento { get; set; }
        public IEnumerable<ConsumoDto>? Consumos { get; set; }
    }
    public class InformacoesClienteDto
    {
        public int Id { get; set; }
        public string? Telefone { get; set; }
        public string? Rg { get; set; }
        public string? Cpf { get; set; }
        public string? Endereco { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
    }
}

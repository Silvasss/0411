namespace _0411.Model
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Auth? Auth { get; set; }
        public int Auth_Id { get; set; }
        public Informacoes_Emprego? InformacoesEmprego { get; set; }
        public Informacoes_Usuario? InformacoesUsuario { get; set; }
        public IEnumerable<Cliente>? Clientes { get; set; }
    }
    public partial class Informacoes_Emprego
    {
        public int Id { get; set; }
        public DateTime? Data_Admissao { get; set; }
        public decimal? Salario { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Usuario? Usuario { get; set; }
        public int Usuario_Id { get; set; }
    }
    public partial class Informacoes_Usuario
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
        public Usuario? Usuario { get; set; }
        public int Usuario_Id { get; set; }
    }
}

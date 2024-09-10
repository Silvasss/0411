namespace _0411.Model
{
    public partial class Estabelecimento
    {
        public int Id { get; set; }
    }
    public partial class Tipo_Quarto
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor_Diaria { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<Quarto>? Quarto { get; set; }
    }
    public partial class Quarto
    {
        public int Id { get; set; }
        public int Numero_Quarto { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Tipo_Quarto? TipoQuarto { get; set; }
        public int TipoQuarto_Id { get; set; }
    }
    public partial class Produto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public decimal? Valor { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<Consumo>? Consumos { get; set; }
    }
}

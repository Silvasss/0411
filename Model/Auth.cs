namespace _0411.Model
{
    public partial class Auth
    {
        public int Id { get; set; }
        public string? User { get; set; }
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TipoConta? TipoConta { get; set; }
        public int TipoConta_Id { get; set; }
        public Usuario? Usuario { get; set; }
    }
    public partial class TipoConta
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<Auth>? Auth { get; set; }
    }
}

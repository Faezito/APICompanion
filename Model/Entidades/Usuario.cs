namespace Model.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public string NomeUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string SenhaHash { get; set; } = null!;
        public DateTime UltimoAcesso { get; set; } = DateTime.Now;
        public Pessoa Pessoa { get; set; } = null!;
    }
}

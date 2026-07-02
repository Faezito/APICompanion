namespace Model.Entidades
{
    public class Pessoa : Auditoria
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
    }
}

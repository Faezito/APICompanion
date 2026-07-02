namespace Model.Entidades
{
    public abstract class Auditoria
    {
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataDeletado {  get; set; }
        public int UsuarioCriacaoId { get; set; }
        public int UsuarioAlteracaoId { get; set; }
    }
}

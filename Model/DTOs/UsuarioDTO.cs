namespace Model.DTOs
{
    public class UsuarioDTOCriacao
    {
        public string NomeCompleto { get; set; } = null!;
        public string NomeUsuario { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public Perfil? perfil { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao => DateTime.Now;
        public int? UsuarioLogadoId { get; }
    }

    public class UsuarioDTOAtualizacao
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public Perfil? Perfil { get; set; }
        public DateTime DataAtualizacao => DateTime.Now;
        public int? UsuarioLogadoId { get; }
    }

    public class UsuarioDTOAtualizacaoDeSenha
    {
        public int UsuarioId { get; set; }
        public string SenhaAtual { get; set; } = null!;
        public string NovaSenha { get; set; } = null!;
        public DateTime DataAtualizacao => DateTime.Now;
        public int? UsuarioLogadoId { get; }
    }

    public class UsuarioDTOResposta
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = null!;
        public string NomeUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public string GeneroTxt => Genero switch
        {
            "M" => "Masculino",
            "F" => "Feminino",
            _ => "Outro"
        };
        public DateTime DataNascimento { get; set; }
        public Perfil Perfil { get; set; }
    }

    public class UsuarioDTODelecao
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public DateTime DataDeletado { get; set; } = DateTime.Now;
    }

    public enum Perfil
    {
        Admin = 1,
        Usuario = 5
    }
}

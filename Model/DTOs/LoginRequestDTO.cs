using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "O campo 'Login' é obrigatório.")]
        public string Login { get; set; } = null!;
        [Required(ErrorMessage = "O campo 'Senha' é obrigatório.")]
        public string Senha { get; set; } = null!;
    }
}
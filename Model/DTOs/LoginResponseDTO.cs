using System;

namespace Model.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration => DateTime.Now.AddHours(3);
        public UsuarioDTOResposta Usuario { get; set; } = null!;
    }
}
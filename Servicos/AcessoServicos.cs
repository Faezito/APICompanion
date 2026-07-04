using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.DTOs;
using Model.Entidades;
using Repositorio.Dados;
using bCrypt = BCrypt.Net.BCrypt;

namespace Servicos
{
    public interface IAcessoServicos
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO dto);
    }

    public class AcessoServicos : IAcessoServicos
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AcessoServicos(AppDbContext db, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO dto)
        {
            var usuario = await _db.Set<Usuario>()
                .Include(u => u.Pessoa)
                .FirstOrDefaultAsync(u => u.Email == dto.Login || u.NomeUsuario == dto.Login);

            if (usuario == null || !bCrypt.Verify(dto.Senha, usuario.SenhaHash))
                throw new Exception("Credenciais inválidas.");
    
            return new LoginResponseDTO
            {
                Token = GerarToken(usuario),
                Usuario = _mapper.Map<UsuarioDTOResposta>(usuario)
            };
        }

        private string GerarToken(Usuario usuario)
        {
            var claims = CriarClaims(usuario);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IEnumerable<Claim> CriarClaims(Usuario usuario)
        {
            return new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, usuario.NomeUsuario),
                new(JwtRegisteredClaimNames.Email, usuario.Email),

                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }
    }
}
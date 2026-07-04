using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Servicos;

namespace APICompanion.Controllers
{
    [ApiController]
    [Route("api/acesso")]
    public class AcessoController : Controller
    {
        private readonly IAcessoServicos _acessoServicos;
        public AcessoController(IAcessoServicos acessoServicos)
        {
            _acessoServicos = acessoServicos;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginDTO)
        {
            var usuario = await _acessoServicos.Login(loginDTO);
            return Ok(usuario);
        }
    }
}

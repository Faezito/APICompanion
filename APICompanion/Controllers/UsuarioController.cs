using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Servicos;

namespace APICompanion.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private IUsuarioServicos _servicoUsuario;
        public UsuarioController(IUsuarioServicos servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        [HttpPost]
        public IActionResult Cadastro(UsuarioDTOCriacao usuarioDTO)
        {
            _servicoUsuario.Cadastro(usuarioDTO);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ObterPorId(int usuarioId)
        {
            var usuario = await _servicoUsuario.ObterPorId(usuarioId);

            return Json(usuario);
        } 
    }
}

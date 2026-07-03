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

        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastro(UsuarioDTOCriacao usuarioDTO)
        {
            await _servicoUsuario.Cadastro(usuarioDTO);
            return Ok();
        }

        [HttpGet("obter")]
        public async Task<IActionResult> ObterPorId(int usuarioId)
        {
            var usuario = await _servicoUsuario.ObterPorId(usuarioId);

            return Json(usuario);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var usuarios = await _servicoUsuario.Listar();

            return Json(usuarios);
        }

        [HttpPut("atualizar-usuario")]
        public async Task<IActionResult> AtualizarUsuario(UsuarioDTOAtualizacao dto)
        {
            await _servicoUsuario.Atualizacao(dto);
            return Ok();
        }

        [HttpPut("atualizar-senha")]
        public async Task<IActionResult> AtualizarSenha(UsuarioDTOAtualizacaoDeSenha dto)
        {
            await _servicoUsuario.AtualizarSenha(dto);
            return Ok();
        }

        [HttpDelete("deletar")]
        public async Task<IActionResult> DeletarUsuario(int usuarioId)
        {
            await _servicoUsuario.DeletarSemExcluir(new UsuarioDTODelecao { Id = usuarioId });
            return Ok();
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entidades;
using Repositorio;
using Repositorio.Dados;
using bCrypt = BCrypt.Net.BCrypt;

namespace Servicos
{
    public interface IUsuarioServicos : ICRUDGenerico<Pessoa>
    {
        Task Cadastro(UsuarioDTOCriacao usuarioDto);
        Task Atualizacao(UsuarioDTOAtualizacao dto);
        Task AtualizarSenha(UsuarioDTOAtualizacaoDeSenha dto);
        Task<List<UsuarioDTOResposta>> Listar();
        Task<UsuarioDTOResposta> ObterPorId(int usuarioId);
        Task<UsuarioDTOResposta> ObterPorEmailAsync(string email);
        Task DeletarSemExcluir(UsuarioDTODelecao dto);
    }
    public class UsuarioServicos : CRUDGenerico<Pessoa>, IUsuarioServicos
    {
        private IMapper mapper;

        public UsuarioServicos(AppDbContext db, IMapper mapper) : base(db)
        {
            this.mapper = mapper;
        }

        public async Task Atualizacao(UsuarioDTOAtualizacao usuarioDTOAtualizacao)
        {
            Usuario usuario = await ObterUsuarioPorId(usuarioDTOAtualizacao.Id)
                ?? throw new Exception("Erro ao atualizar usuario: usuário não encontrado");
            usuario.Email = usuarioDTOAtualizacao.Email;
            await SalvarAsync();
        }

        public async Task AtualizarSenha(UsuarioDTOAtualizacaoDeSenha dto)
        {
            if (dto.SenhaAtual == dto.NovaSenha) throw new Exception("A senha atual não pode ser igual à anterior.");
            var usuario = await _db.Set<Usuario>().FirstOrDefaultAsync(x => x.Id == dto.UsuarioId);

            usuario.SenhaHash = bCrypt.HashPassword(dto.NovaSenha);
            _db.Set<Usuario>().Update(usuario);
            await _db.SaveChangesAsync();
        }

        public async Task Cadastro(UsuarioDTOCriacao usuarioDto)
        {
            var pessoa = mapper.Map<Pessoa>(usuarioDto);

            var usuario = mapper.Map<Usuario>(usuarioDto);
            usuario.SenhaHash = bCrypt.HashPassword(usuarioDto.Senha);

            pessoa.Usuario = usuario;

            await _dbSet.AddAsync(pessoa);
            await _db.Set<Usuario>().AddAsync(usuario);

            await SalvarAsync();
        }

        public async Task DeletarSemExcluir(UsuarioDTODelecao dto)
        {
            var usuario = await ObterUsuarioPorId(dto.Id);
            if (usuario == null) throw new Exception("Erro ao excluir usuário: usuário não encontrado");

            var pessoa = await ObterPessoaPorId(usuario.PessoaId);
            if (pessoa == null) throw new Exception("Erro ao excluir usuário: pessoa não encontrada");

            _db.Set<Usuario>().Remove(usuario);
            _dbSet.Remove(pessoa);

            await SalvarAsync();
        }

        public async Task<UsuarioDTOResposta> ObterPorEmailAsync(string email)
        {
            var usuario = await _db.Set<Usuario>()
                .Include(u => u.Pessoa)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (usuario == null) { throw new Exception("Usuario não encontrado"); }
            return mapper.Map<UsuarioDTOResposta>(usuario);
        }

        public async Task<UsuarioDTOResposta> ObterPorId(int usuarioId)
        {
            var usuario = await _db.Set<Usuario>()
                .Include(u => u.Pessoa)
                .FirstOrDefaultAsync(x => x.Id == usuarioId);

            if (usuario == null) { throw new Exception("Usuario não encontrado"); }
            return mapper.Map<UsuarioDTOResposta>(usuario);
        }

        public async Task<List<UsuarioDTOResposta>> Listar()
        {
            var usuario = await _db.Set<Usuario>()
                .Include(u => u.Pessoa)
                .ToListAsync();

            if (usuario == null) { throw new Exception("Usuario não encontrado"); }
            return mapper.Map<List<UsuarioDTOResposta>>(usuario);
        }

        private async Task<Usuario?> ObterUsuarioPorId(int usuarioId) =>
            await _db.Set<Usuario>().FirstOrDefaultAsync(x => x.Id == usuarioId);

        private async Task<Pessoa?> ObterPessoaPorId(int pessoaId) =>
            await _dbSet.FirstOrDefaultAsync(x => x.Id == pessoaId);
    }
}

using Newtonsoft.Json;
using Repositorio;
using Repositorio.Dados;

namespace Servicos
{
    public interface ILogDeErroServicos : ICRUDGenerico<LogDeErro>
    {
        Task LogarErro(Exception ex, string path);
        Task Deletar(int id);
    }

    public class LogDeErroServicos : CRUDGenerico<LogDeErro>, ILogDeErroServicos
    {
        public LogDeErroServicos(AppDbContext context) : base(context) { }
        public async Task Deletar(int id)
        {
            var erro = await ObterPorIdAsync(id);
            if (erro == null) return;
            Remover(erro);
            await SalvarAsync();
        }
        public async Task LogarErro(Exception ex, string path)
        {
            LogDeErro log = new LogDeErro
            {
                Mensagem = ex.Message,
                Url = path,
                StackTrace = JsonConvert.SerializeObject(ex),
                Data = DateTime.Now
            };
            await AdicionarAsync(log);
        }
    }
}
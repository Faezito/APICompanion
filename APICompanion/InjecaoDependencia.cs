using Repositorio;
using Servicos;

namespace APICompanion
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection InjecaoServicos(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioServicos, UsuarioServicos>();
            services.AddScoped<IAcessoServicos, AcessoServicos>();
            services.AddScoped<ILogDeErroServicos, LogDeErroServicos>();
            return services;
        }

        public static IServiceCollection InjecaoRepositorios(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICRUDGenerico<>), typeof(CRUDGenerico<>));
            return services;
        }
    }
}



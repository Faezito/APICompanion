using Repositorio;
using Servicos;

namespace APICompanion
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection InjecaoServicos(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioServicos, UsuarioServicos>();
            return services;
        }

        public static IServiceCollection InjecaoRepositorios(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICRUDGenerico<>), typeof(CRUDGenerico<>));
            return services;
        }
    }
}



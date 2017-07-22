

using CrossCutting.Identity.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Stone.Data.Implementation;
using Stone.Data.Interfaces;

namespace CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEstabelecimentoRepository, EstabelecimentoRepository>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<ICustomJwtSecurityToken, CustomJwtSecurityToken>();
        }
    }
}
using ProyectoWeb.Domain.Services.Interface;
using ProyectoWeb.Domain.Services;

namespace ProyectoWeb.Handlers
{
    public static class DependencyInyectionHandler
    {
        public static void DependencyInyectionConfig(IServiceCollection services)
        {
            //Domain
            services.AddTransient<IUserServices, UserServices>();
           
        }
    }
}

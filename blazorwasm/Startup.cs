using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

using blazor.Store;

namespace blazor
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AppState>();
        }


        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}

using GridCraftTableGenDotNetWebApi.GridGeneration;

namespace GridCraftTableGenDotNetWebApi
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            // Transient services are created each time they are requested
            builder.Services.AddTransient<GridGenerationService>();
        }
    }
}
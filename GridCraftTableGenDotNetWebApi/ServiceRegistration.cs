using GridCraftTableGenDotNetWebApi.GridGeneration;
using Amazon.S3;

namespace GridCraftTableGenDotNetWebApi
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            // Transient services are created each time they are requested
            builder.Services.AddTransient<GridGenerationService>();

            // Register AWS services
            builder.Services.AddAWSService<IAmazonS3>();
        }
    }
}
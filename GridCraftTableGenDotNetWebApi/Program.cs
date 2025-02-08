using GridCraftTableGenDotNetWebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteConvention());
});

// Add OpenAPI (Swagger) services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // <-- Enable Swagger middleware
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); // Link to the generated Swagger JSON
        options.RoutePrefix = string.Empty; // Optional: to make the Swagger UI available at the root ("/")
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
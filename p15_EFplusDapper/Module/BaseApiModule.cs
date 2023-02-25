using p15_EFplusDapper.Infrastructure;
using p15_EFplusDapper.Infrastructure.Data;

namespace p15_EFplusDapper.Module;

public static class BaseApiModule
{
    public static void AddBaseApiModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void UseBaseApiModule(this IApplicationBuilder app, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();

        app.UseEndpoints(end => end.MapControllers());
    }

    public static void Seed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DatabaseContext>();
            ContextInitializer.Initialize(context);
        }
        catch (Exception ex)
        {

        }
    }
}
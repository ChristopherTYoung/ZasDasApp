using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;
using ZasAndDasWeb.Components;
using ZasUndDas.Shared.Data;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents();

        builder.Services.AddControllers();
        builder.Services.AddDbContext<PostgresContext>(o => o.UseNpgsql(builder.Configuration["DB_CONN"]));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        var swaggerIsVisible = builder.Configuration.GetValue<bool?>("SHOW_SWAGGER") ?? false;

        if (swaggerIsVisible)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.MapControllers();
        app.UseHttpsRedirection();
#if Swagger
        app.UseRouting();    
#endif

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>();

        app.Run();
    }
}
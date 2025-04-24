using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Resources;
using OpenTelemetry.Logs;
using OpenTelemetry.Exporter;
using Swashbuckle.AspNetCore;
using ZasAndDasWeb.Components;
using ZasUndDas.Shared.Data;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using ZasAndDasWeb.Services;
using Azure.Storage.Blobs;
using Square;
using System.Buffers.Text;
using System.Diagnostics.Metrics;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var blobConnectionString = builder.Configuration["BLOB_CONN"];
        if (blobConnectionString != null)
        {
            BlobServiceClient blobService = new BlobServiceClient(blobConnectionString);
            builder.Services.AddSingleton(_ => blobService.GetBlobContainerClient("menuitemimages"));
            builder.Services.AddSingleton<BlobService>();
        }

        builder.Services.AddRazorComponents();

        builder.Services.AddSingleton(sp =>
        {
            return new SquareClient(
                token: builder.Configuration["square_token"],
                clientOptions: new ClientOptions
                {
                    BaseUrl = SquareEnvironment.Sandbox
                }
            );
        });
        builder.Services.AddSingleton<PaymentService>();
        builder.Services.AddMetrics();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<PostgresContext>(o => o.UseNpgsql(builder.Configuration["DB_CONN"]));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<OrderService>();
        builder.Services.AddScoped<IAPIKeyValidationService, APIKeyValidationService>();

        var collectorURL = builder.Configuration["COLLECTOR_URL"] ?? null;

        if (collectorURL != null)
        {
            var resourceBuilder = ResourceBuilder
                .CreateDefault()
                .AddService("TelemetryAspireDashboardQuickstart");

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                    resource.AddService(serviceName: MetricService.MeterName)
                )
                .WithMetrics(metrics => metrics
                    .AddMeter(MetricService.MeterName)
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri(collectorURL);
                    }))
                .WithTracing(tracing => tracing
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri(collectorURL);
                    }));
            builder.Services.AddSingleton<MetricService>();
            builder.Logging.AddOpenTelemetry(options =>
            {
                options.SetResourceBuilder(resourceBuilder);
                // options.AddOtlpExporter(options => options.Endpoint = new Uri(uriString: builder.Configuration["ASPIRE_DASHBOARD_URL"]));
                options.AddOtlpExporter(options => options.Endpoint = new Uri(collectorURL));
                options.IncludeFormattedMessage = true;
                options.IncludeScopes = true;
            });
        }

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
        // this is breaking things for some reason
        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>();

        app.Run();
    }
}


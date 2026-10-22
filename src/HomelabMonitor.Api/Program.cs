using HomelabMonitor.Api.Configuration;
using HomelabMonitor.Api.Clients;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOptions<ProxmoxOptions>()
    .Bind(builder.Configuration.GetSection(ProxmoxOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHttpClient<ProxmoxClient>((serviceProvider, client) =>
{
    var proxmoxOptions = serviceProvider.GetRequiredService<IOptions<ProxmoxOptions>>().Value;
    client.BaseAddress = new Uri(proxmoxOptions.BaseUrl);
    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"PVEAPIToken={proxmoxOptions.TokenId}={proxmoxOptions.TokenSecret}");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/proxmox/resources", async (ProxmoxClient proxmoxClient) =>
{
    var resources = await proxmoxClient.GetResourcesAsync(cancellationToken: default);
    return Results.Ok(resources);
});

app.Run();

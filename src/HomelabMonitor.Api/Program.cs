using HomelabMonitor.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOptions<ProxmoxOptions>()
    .Bind(builder.Configuration.GetSection(ProxmoxOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.Run();

using HotelService.API;
using HotelService.API.Services;
using HotelService.Application;
using HotelService.Application.Options;
using HotelService.Infrastructure;
using HotelService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions") 
                                       ?? throw new InvalidOperationException("JwtOptions not found"));

builder.Services.AddControllers();
builder.Services.AddSwaggerGenWithBearer();
builder.Services.AddJwtAuthentication(builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()
                                      ?? throw new InvalidOperationException("JwtOptions not found"));
builder.Services.AddAuthorization();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHostedService<OutboxWorker>();

var app = builder.Build();

await Migrator.MigrateAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
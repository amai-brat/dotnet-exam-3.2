using HotelService.API;
using HotelService.Application.Options;
using HotelService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions") 
                                       ?? throw new InvalidOperationException("JwtOptions not found"));

builder.Services.AddControllers();
builder.Services.AddSwaggerGenWithBearer();
builder.Services.AddJwtAuthentication(builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()
                                      ?? throw new InvalidOperationException("JwtOptions not found"));
builder.Services.AddAuthorization();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

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
using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Data.Abstractions;
using PaymentService.Data.Impls;
using PaymentService.Services;
using PaymentService.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAccountProvider, FakeAccountProvider>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(b =>
{
    b.UseNpgsql(builder.Configuration.GetConnectionString("PostgresPayment"));
});

builder.Services
    .AddGraphQLServer()
    .RegisterDbContextFactory<AppDbContext>()
    .AddMutationConventions(new MutationConventionOptions
    {
        PayloadErrorsFieldName = "errors",
        ApplyToAllMutations = true
    })
    .AddQueryType<Query>()
    .AddMutationType()
        .AddTypeExtension<PaymentMutation>();

var app = builder.Build();

await Migrator.MigrateAsync(app.Services);

app.MapGraphQL();
app.Run();
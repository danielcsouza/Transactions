using Microsoft.EntityFrameworkCore;
using Transactions.Persistence;
using Transactions.Persistence.Repositories;
using Transactions.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<ITransaction, DepositTransaction>();
builder.Services.AddScoped<ITransaction, WithDrawTransaction>();
builder.Services.AddScoped<ITransaction, TransferTransaction>();

builder.Services.AddDbContext<TransactionContext>(options => options.UseInMemoryDatabase("Transactions"));


var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

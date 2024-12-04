using GerenciamentoPedidosFornecedores.Data;
using GerenciamentoPedidosFornecedores.Models;
using GerenciamentoPedidosFornecedores.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registre o repositório para Pedido
builder.Services.AddScoped<IRepository<Pedido>, PedidoRepository>();

// Registre o repositório para Fornecedor (se houver)
builder.Services.AddScoped<IRepository<Fornecedor>, FornecedorRepository>();


var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

using GerenciamentoPedidosFornecedores.Data;
using GerenciamentoPedidosFornecedores.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPedidosFornecedores.Repositories;

public class FornecedorRepository : IRepository<Fornecedor>
{
    private readonly AppDbContext _context;

    public FornecedorRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Fornecedor> GetAll() => _context.Fornecedores.ToList();

    public Fornecedor GetById(int id) => _context.Fornecedores.Find(id);

    public void Add(Fornecedor entity)
    {
        _context.Fornecedores.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Fornecedor entity)
    {
        _context.Fornecedores.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var fornecedor = _context.Fornecedores.Find(id);
        if (fornecedor != null)
        {
            _context.Fornecedores.Remove(fornecedor);
            _context.SaveChanges();
        }
    }
}

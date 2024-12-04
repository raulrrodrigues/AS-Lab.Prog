using GerenciamentoPedidosFornecedores.Data;
using GerenciamentoPedidosFornecedores.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPedidosFornecedores.Repositories;

public class PedidoRepository : IRepository<Pedido>
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Pedido> GetAll() => _context.Pedidos.ToList();

    public Pedido GetById(int id) => _context.Pedidos.Find(id);

    public void Add(Pedido entity)
    {
        _context.Pedidos.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Pedido entity)
    {
        _context.Pedidos.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var pedido = _context.Pedidos.Find(id);
        if (pedido != null)
        {
            _context.Pedidos.Remove(pedido);
            _context.SaveChanges();
        }
    }
}

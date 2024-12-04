using GerenciamentoPedidosFornecedores.Models;
using GerenciamentoPedidosFornecedores.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPedidosFornecedores.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IRepository<Pedido> _repository;

    public PedidosController(IRepository<Pedido> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repository.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id) => Ok(_repository.GetById(id));

    [HttpPost]
    public IActionResult Add(Pedido pedido)
    {
        _repository.Add(pedido);
        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id}")]
public IActionResult UpdatePedido(int id, [FromBody] Pedido pedidoAtualizado)
{
    if (pedidoAtualizado == null)
    {
        return BadRequest("Pedido não fornecido.");
    }

    // Buscar o pedido pelo ID
    var pedidoExistente = _repository.GetById(id);
    if (pedidoExistente == null)
    {
        return NotFound($"Pedido com ID {id} não encontrado.");
    }

    // Atualizar as propriedades do pedido existente com os dados fornecidos
    pedidoExistente.Data = pedidoAtualizado.Data;
    pedidoExistente.ValorTotal = pedidoAtualizado.ValorTotal;
    pedidoExistente.Status = pedidoAtualizado.Status;
    pedidoExistente.Descricao = pedidoAtualizado.Descricao;

    // Salvar as alterações no banco de dados
    _repository.Update(pedidoExistente);

    return NoContent(); // Retorna 204 No Content, indicando que a atualização foi bem-sucedida
}


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repository.Delete(id);
        return NoContent();
    }
}

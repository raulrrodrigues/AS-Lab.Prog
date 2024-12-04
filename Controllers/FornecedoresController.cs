using GerenciamentoPedidosFornecedores.Models;
using GerenciamentoPedidosFornecedores.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPedidosFornecedores.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedoresController : ControllerBase
{
    private readonly IRepository<Fornecedor> _repository;

    public FornecedoresController(IRepository<Fornecedor> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repository.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id) => Ok(_repository.GetById(id));

    [HttpPost]
    public IActionResult Add(Fornecedor fornecedor)
    {
        _repository.Add(fornecedor);
        return CreatedAtAction(nameof(GetById), new { id = fornecedor.Id }, fornecedor);
    }

    [HttpPut("{id}")]
public IActionResult UpdateFornecedor(int id, [FromBody] Fornecedor fornecedorAtualizado)
{
    if (fornecedorAtualizado == null)
    {
        return BadRequest("Fornecedor não fornecido.");
    }

    // Buscar o fornecedor pelo ID
    var fornecedorExistente = _repository.GetById(id);
    if (fornecedorExistente == null)
    {
        return NotFound($"Fornecedor com ID {id} não encontrado.");
    }

    // Atualizar as propriedades do fornecedor existente com os dados fornecidos
    fornecedorExistente.Nome = fornecedorAtualizado.Nome;
    fornecedorExistente.CNPJ = fornecedorAtualizado.CNPJ;
    fornecedorExistente.Telefone = fornecedorAtualizado.Telefone;
    fornecedorExistente.Email = fornecedorAtualizado.Email;
    fornecedorExistente.Endereco = fornecedorAtualizado.Endereco;

    // Salvar as alterações no banco de dados
    _repository.Update(fornecedorExistente);

    return NoContent(); // Retorna 204 No Content, indicando que a atualização foi bem-sucedida
}


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repository.Delete(id);
        return NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;
using CrudClientes.ApiService.Models;
using CrudClientes.ApiService.Repositories;

namespace CrudClientes.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            try
            {
                var clientes = _clienteRepository.GetAll();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar clientes: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            try
            {
                var cliente = _clienteRepository.GetById(id);
                if (cliente == null)
                {
                    return NotFound(new { Mensagem = "Cliente não encontrado.", Id = id });
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar cliente: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<Cliente> Add([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _clienteRepository.Add(cliente);
                return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao adicionar cliente: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest(new { Mensagem = "O ID do cliente não corresponde ao ID fornecido na URL." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var clienteExistente = _clienteRepository.GetById(id);
                if (clienteExistente == null)
                {
                    return NotFound(new { Mensagem = "Cliente não encontrado para atualização.", Id = id });
                }

                _clienteRepository.Update(cliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar cliente: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var clienteExistente = _clienteRepository.GetById(id);
                if (clienteExistente == null)
                {
                    return NotFound(new { Mensagem = "Cliente não encontrado para exclusão.", Id = id });
                }

                _clienteRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao excluir cliente: {ex.Message}");
            }
        }
    }
}
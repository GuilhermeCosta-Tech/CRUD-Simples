using Microsoft.AspNetCore.Mvc;
using CrudClientes.ApiService.Models;
using CrudClientes.ApiService.Repositories;

/*Resumo breve: Este arquivo é responsável por gerenciar requisições HTTP.
 * Interação com o repositório para acesso e manipulação de dados.
 * Validação de dados recebido nas requisições.
 Retornar respostas apropriadas.
*/

namespace CrudClientes.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)  //Construtor utilizado para acessar e/ou manipular os dados dos clientes
        {
            _clienteRepository = clienteRepository;
        }

        /*Responde as requisições HTTP GET no endpoint /api/clientes
        Chama o método GetAllClients do repositório para obter todos os clientes com dois possiveis retornos: em caso de sucesso(todos clientes encontrados na lista), retorna um OK. Caso haja alguma exceção, retorna uma mensagem de erro.*/
        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            try
            {
                var clientes = _clienteRepository.GetAllClients();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar clientes: {ex.Message}");
            }
        }

        /*Responde a requisições GTTP GET no endpoint /api/clientes/{id}
        Chama o método GetById no repositório para buscar um cliente pelo ID.
        Contem três tipos de retornos, o primeiro sendo um OK caso ocorra tudo como desejado e todos clientes sejam encontrados. 
        O segundo sendo o famoso 404 (Not Found) caso o cadastro do cliente não exista na lista
        O terceiro retorna uma mensagem de erro (505) caso ocorra uma exceção*/
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


        /*Responde a requisição HTTP POST no endpoint /api/clientes
         Recebe os dados do cliente no corpo da requisição (FromBody)
        Valida os dados usando o ModelState, logo após chama o método Add para adicionar o cliente*/
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


        /*Responde a requisições HTTP PUT no endpoint /api/clientes/{id}
         Recebe os dados do cliente no corpo da requisição (FromBody)
        Valida se o ID na URL corresponde ao do cliente
        Valida os dados utilizando o ModelState
        Chama o método update para atualizar o cliente*/
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
       /*Responde a requisições HTTP DELETE no endpoint /api/clientes/{id}
        Chama o método GetById do repositório para verificar se o cliente existe
        Chama o método Delete para excluir a conta do cliente*/
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
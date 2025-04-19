using CrudClientes.ApiService.Models;

namespace CrudClientes.ApiService.Repositories
{
    // Interface que define as opera��es CRUD para a entidade Cliente
    public interface IClienteRepository
    {
       List<Cliente> GetAllClients();
        Cliente GetById(int id);
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(int id);

    }
}
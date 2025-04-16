using CrudClientes.ApiService.Models;

namespace CrudClientes.ApiService.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> _clientes = new();

        public List<Cliente> GetAll()
        {
            return _clientes;
        }

        public Cliente GetById(int id)
        {
            return _clientes.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Cliente cliente)
        {
            cliente.Id = _clientes.Count > 0 ? _clientes.Max(c => c.Id) + 1 : 1;
            _clientes.Add(cliente);
        }

        public void Update(Cliente cliente)
        {
            var existente = GetById(cliente.Id);
            if (existente != null)
            {
                existente.Nome = cliente.Nome;
                existente.Email = cliente.Email;
                existente.Telefone = cliente.Telefone;
                existente.DataAtualizacao = DateTime.UtcNow;
            }
        }

        public void Delete(int id)
        {
            var cliente = GetById(id);
            if (cliente != null)
            {
                _clientes.Remove(cliente);
            }
        }
    }
}

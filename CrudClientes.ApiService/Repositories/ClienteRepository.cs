using CrudClientes.ApiService.Models;

namespace CrudClientes.ApiService.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> _clientes = new();

        public List<Cliente> GetAllClients() 
        {
            return _clientes.ToList();
        }

        public bool ValidarCPF(string CPF)
        {
            if (string.IsNullOrWhiteSpace(CPF) || CPF.Length != 11 || !CPF.All(char.IsDigit))
                return false;

            var cpfNumeros = CPF.Select(c => int.Parse(c.ToString())).ToArray();
            var digito1 = CalcularDigitoVerificador(cpfNumeros, 10);
            var digito2 = CalcularDigitoVerificador(cpfNumeros, 11);

            return cpfNumeros[9] == digito1 && cpfNumeros[10] == digito2;
        }

        private static int CalcularDigitoVerificador(int[] numeros, int pesoInicial)
        {
            var soma = 0;
            for (int i = 0; i < pesoInicial - 1; i++)
            {
                soma += numeros[i] * (pesoInicial - i);
            }

            var resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }

        public Cliente GetById(int id)
        {
            return _clientes.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Cliente cliente)
        {
            try
            {
                if (!ValidarCPF(cliente.CPF))
                    throw new ArgumentException("CPF inválido.");

                cliente.Id = _clientes.Count > 0 ? _clientes.Max(c => c.Id) + 1 : 1;
                cliente.DataCriacao = DateTime.UtcNow;
                _clientes.Add(cliente);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao adicionar cliente: {ex.Message}", ex);
            }
        }

        public void Update(Cliente cliente)
        {
            try
            {
                var existente = GetById(cliente.Id);
                if (existente == null)
                    throw new KeyNotFoundException("Cliente não encontrado para atualização.");

                if (!ValidarCPF(cliente.CPF))
                    throw new ArgumentException("CPF inválido.");

                existente.Nome = cliente.Nome;
                existente.Email = cliente.Email;
                existente.Telefone = cliente.Telefone;
                existente.DataAtualizacao = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar cliente: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var cliente = GetById(id);
                if (cliente == null)
                    throw new KeyNotFoundException("Cliente não encontrado para exclusão.");

                _clientes.Remove(cliente);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao excluir cliente: {ex.Message}", ex);
            }
        }
    }
}

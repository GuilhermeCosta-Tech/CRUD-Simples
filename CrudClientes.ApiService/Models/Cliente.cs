using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CrudClientes.ApiService.Models
{
    // Classe com requisições obrigatórias para o cliente
    public class Cliente
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve ser um endereço de email válido.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "O telefone deve ser válido.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        public string CPF { get; set; } = string.Empty;

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataAtualizacao { get; set; }

        [Required]
        public bool Ativo { get; set; } = true;
        public int Id { get; internal set; }

        public bool ValidarCPF()
        {
            if (string.IsNullOrWhiteSpace(CPF) || CPF.Length != 11 || !Regex.IsMatch(CPF, @"^\d{11}$"))
                return false;

            var cpfNumeros = CPF.Select(c => int.Parse(c.ToString())).ToArray();
            var digito1 = CalcularDigitoVerificador(cpfNumeros, 10);
            var digito2 = CalcularDigitoVerificador(cpfNumeros, 11);

            return cpfNumeros[9] == digito1 && cpfNumeros[10] == digito2;
        }

        private int CalcularDigitoVerificador(int[] numeros, int pesoInicial)
        {
            var soma = 0;
            for (int i = 0; i < pesoInicial - 1; i++)
            {
                soma += numeros[i] * (pesoInicial - i);
            }

            var resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}

# CrudClientes.ApiService

## 📖 Descrição
O `CrudClientes.ApiService` é uma API RESTful desenvolvida em .NET 8 que permite gerenciar clientes. Ele oferece operações de **CRUD** (Criar, Ler, Atualizar e Deletar) para clientes, com validações de dados e suporte a testes automatizados.

---

## 🚀 Funcionalidades
- **Listar Clientes**: Obtenha uma lista de todos os clientes cadastrados.
- **Buscar Cliente por ID**: Consulte os detalhes de um cliente específico.
- **Adicionar Cliente**: Cadastre novos clientes com validações de dados.
- **Atualizar Cliente**: Atualize as informações de um cliente existente.
- **Excluir Cliente**: Remova um cliente do sistema.

---

## 🛠️ Tecnologias Utilizadas
- **.NET 8**
- **C# 12**
- **Blazor** (prioridade no projeto)
- **xUnit** (para testes automatizados)
- **Moq** (para mocks em testes)
- **Microsoft.AspNetCore.Mvc.Testing** (para testes de controladores)

---

## 📂 Estrutura do Projeto
```
CrudClientes.ApiService/
 ├── Controllers/
 │
 └── ClientesController.cs # Controlador principal da API
 ├── Models/
 │
 └── Cliente.cs # Modelo de dados do cliente
 ├── Repositories/
 │
 ├── IClienteRepository.cs # Interface do repositório
 │
 └── ClienteRepository.cs # Implementação do repositório
 ├── Program.cs # Configuração principal da aplicação
 └── Tests/
 └── ClientesControllerTests.cs # Testes automatizados do controlador 
---
```
## ⚙️ Configuração e Execução

### Pré-requisitos
- **.NET SDK 8.0** ou superior
- **Visual Studio 2022** ou outro editor compatível com .NET

### Passos para Executar
1. Clone o repositório:   git clone https://github.com/GuilhermeCosta-Tech/CrudClientes.ApiService.git cd CrudClientes.ApiService
	
2. Restaure os pacotes NuGet:   dotnet restore

3. Execute o projeto:   dotnet run --project CrudClientes.ApiService
   
4. Acesse a API em:   http://localhost:5000/api/clientes

   
---

## 🧪 Testes Automatizados

### Executando os Testes
  1. Navegue até o diretório de testes:  cd CrudClientes.Tests
   
  2. Execute os testes:  dotnet test

  3. Verifique os resultados no terminal.

---

## 📋 Endpoints da API

### Clientes
| Método | Endpoint               | Descrição                          |
|--------|------------------------|------------------------------------|
| GET    | `/api/clientes`        | Lista todos os clientes.           |
| GET    | `/api/clientes/{id}`   | Busca um cliente pelo ID.          |
| POST   | `/api/clientes`        | Adiciona um novo cliente.          |
| PUT    | `/api/clientes/{id}`   | Atualiza um cliente existente.     |
| DELETE | `/api/clientes/{id}`   | Remove um cliente pelo ID.         |

---

## 🛡️ Validações de Dados
O modelo `Cliente` possui as seguintes validações:
- **Nome**: Obrigatório, máximo de 100 caracteres.
- **Email**: Obrigatório, deve ser um email válido.
- **Telefone**: Deve ser um número de telefone válido.
- **CPF**: Obrigatório, deve conter 11 caracteres.

---

## 📚 Aprendizados
Este projeto foi desenvolvido como parte de um aprendizado em:
- **POO (Programação Orientada a Objetos)** com foco em boas práticas.
- **Blazor** e desenvolvimento de APIs RESTful.
- **Testes Automatizados** com xUnit e Moq.

---

## 🤝 Contribuições
Contribuições são bem-vindas! Siga os passos abaixo para contribuir:
1. Faça um fork do repositório.
2. Crie uma branch para sua feature: git checkout -b minha-feature
3. Commit suas mudanças: git commit -m "Adiciona minha feature"
4. Envie para o repositório remoto: git push origin minha-feature
5. Abra um Pull Request.

---

## 📄 Licença
Este projeto está licenciado sob a [MIT License](LICENSE).

---

## ✨ Autor
Desenvolvido por **GuilhermeCosta-Tech** como parte do aprendizado em desenvolvimento de software com base nos aprendizados absorvidos da formação: Aprendenda a programar em C# com Orientação a Objetos da plataforma Alura.


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net.Http;
using WebApiParanaBanco.Controllers;
using WebApiParanaBanco.Models;
using WebApiParanaBanco.Repositorio;

namespace TesteWebApiParanaBanco
{
    public class ClientesControllerTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ClientesController _controller;

        public ClientesControllerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _controller = new ClientesController(_clienteRepositoryMock.Object);
        }

        public void CreateCliente_ValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var cliente = new Cliente
            {
                NomeCompleto = "João Silva",
                Email = "joao@example.com",
                Telefones = new List<Telefone>
            {
                new Telefone { DDD = 11, Numero = 999999999, Tipo ="Celular" },
                new Telefone { DDD = 11, Numero = 888888888, Tipo ="Fixo" }
            }
            };

            // Act
            var result = _controller.Criar(cliente) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equals(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equals(nameof(ClientesController.ObterTodos), result.ActionName);
            Assert.Equals(cliente.Id, result.RouteValues["id"]);

        }


    }

}
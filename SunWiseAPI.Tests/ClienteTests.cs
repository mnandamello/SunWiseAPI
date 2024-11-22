using Moq;
using SunWiseAPI.Data;
using SunWiseAPI.Models;
using SunWiseAPI.Repositories.Implementation;
using SunWiseAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SunWiseAPI.Tests
{
    public class ClienteTestes
    {
        private readonly DataContext _context;
        private readonly ClienteRepository _clienteRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;

        public ClienteTestes()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);
            _mockUserRepository = new Mock<IUserRepository>();
            _clienteRepository = new ClienteRepository(_mockUserRepository.Object, _context);
        }

        [Fact]
        public async Task GetCliente_ReturnsCliente_WhenClienteExists()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Nome = "Test", Email = "test@gmail.com", Endereco = "Rua oi", Telefone = "1198789675", UserId = "1" };
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            // Act
            var result = await _clienteRepository.GetCliente(1);

            // Assert
            Assert.Equal(cliente.Nome, result.Nome);
            Assert.Equal(cliente.Email, result.Email);
        }

        [Fact]
        public async Task AddCliente_AddsAndReturnsCliente_WhenUserExists()
        {
            // Arrange
            var cliente = new Cliente { Id = 2, Nome = "Test", Email = "test@gmail.com", Endereco = "Rua oi", Telefone = "1198789675", UserId = "2" };
            var user = new User { Uid = "user-id-1" };

            _mockUserRepository.Setup(m => m.GetUserById("user-id-1")).ReturnsAsync(user);

            // Act
            var result = await _clienteRepository.AddCliente(cliente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Nome, result.Nome);
            Assert.Equal(1, _context.Clientes.Count());
        }

        [Fact]
        public async Task GetClientes_ReturnsAllClientes()
        {
            // Arrange
            _context.Clientes.RemoveRange(_context.Clientes);
            _context.SaveChanges();

            _context.Clientes.Add(new Cliente { Id = 3, Nome = "Test 3", Email = "test3@gmail.com", Endereco = "Rua oi", Telefone = "1198789675", UserId = "3" });
            _context.Clientes.Add(new Cliente { Id = 4, Nome = "Test 4", Email = "test4@gmail.com", Endereco = "Rua Hi", Telefone = "1197748478", UserId = "4" });
            _context.Clientes.Add(new Cliente { Id = 5, Nome = "Test 5", Email = "test5@gmail.com", Endereco = "Rua By", Telefone = "1190991729", UserId = "5" });
            _context.SaveChanges();

            // Act
            var result = await _clienteRepository.GetClientes();

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Collection(result,
                item => Assert.Equal("Test 3", item.Nome),
                item => Assert.Equal("Test 4", item.Nome),
                item => Assert.Equal("Test 5", item.Nome));
        }
    }
}
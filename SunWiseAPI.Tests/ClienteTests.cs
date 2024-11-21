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
        [Fact]
        public async Task GetClientes_ReturnsListOfClientes()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                new Cliente { Id = 1, Nome = "Cliente 1", UserId = "1" },
                new Cliente { Id = 2, Nome = "Cliente 2", UserId = "2" }
            };

            var mockSet = new Mock<DbSet<Cliente>>();
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.Provider).Returns(clientes.AsQueryable().Provider);
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.Expression).Returns(clientes.AsQueryable().Expression);
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.ElementType).Returns(clientes.AsQueryable().ElementType);
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.GetEnumerator()).Returns(clientes.AsQueryable().GetEnumerator());

            mockSet.Setup(x => x.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientes);

            var mockDataContext = new Mock<DataContext>();
            mockDataContext.Setup(x => x.Clientes).Returns(mockSet.Object);

            var repository = new ClienteRepository(Mock.Of<IUserRepository>(), mockDataContext.Object);

            // Act
            var result = await repository.GetClientes();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(clientes.Count, result.Count());
            Assert.Equal(clientes, result);
        }

        [Fact]
        public async Task GetCliente_ExistingId_ReturnsCliente()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Nome = "Cliente 1", UserId = "1" };

            var clientes = new List<Cliente> { cliente };

            var mockSet = new Mock<DbSet<Cliente>>();
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.Provider).Returns(clientes.AsQueryable().Provider);
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.Expression).Returns(clientes.AsQueryable().Expression);
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.ElementType).Returns(clientes.AsQueryable().ElementType);
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.GetEnumerator()).Returns(clientes.AsQueryable().GetEnumerator());

            mockSet.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Cliente, bool>>>(), default))
                .ReturnsAsync(cliente);

            var mockDataContext = new Mock<DataContext>();
            mockDataContext.Setup(x => x.Clientes).Returns(mockSet.Object);

            var repository = new ClienteRepository(Mock.Of<IUserRepository>(), mockDataContext.Object);

            // Act
            var result = await repository.GetCliente(cliente.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Id, result.Id);
            Assert.Equal(cliente.Nome, result.Nome);
        }

        [Fact]
        public async Task AddCliente_ValidCliente_ReturnsCliente()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Nome = "Cliente 1", UserId = "1" };
            var user = new User { Uid = "1", NomeEmpresa = "Empresa X" };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserById(cliente.UserId)).ReturnsAsync(user);

            var clientes = new List<Cliente>();
            var mockSet = new Mock<DbSet<Cliente>>();
            mockSet.Setup(x => x.AddAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Cliente c, CancellationToken ct) =>
                {
                    clientes.Add(c);
                    return Mock.Of<EntityEntry<Cliente>>(e => e.Entity == c);
                });

            var mockDataContext = new Mock<DataContext>();
            mockDataContext.Setup(x => x.Clientes).Returns(mockSet.Object);
            mockDataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var repository = new ClienteRepository(mockUserRepository.Object, mockDataContext.Object);

            // Act
            var result = await repository.AddCliente(cliente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Id, result.Id);
            Assert.Equal(cliente.Nome, result.Nome);
            mockDataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
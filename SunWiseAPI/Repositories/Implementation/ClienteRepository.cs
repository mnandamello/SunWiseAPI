using Microsoft.EntityFrameworkCore;
using SunWiseAPI.Data;
using SunWiseAPI.Models;

namespace SunWiseAPI.Repositories.Implementation
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IUserRepository userRepository;
        private readonly DataContext dataContext;

        public ClienteRepository(IUserRepository userRepository, DataContext dataContext)
        {
            this.userRepository = userRepository;
            this.dataContext = dataContext;
        }

        public async Task<Cliente> AddCliente(Cliente cliente)
        {
            var user = userRepository.GetUserById(cliente.UserId);

            if (user == null) return null;

            var result = await dataContext.Clientes.AddAsync(cliente);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }


        public async Task<Cliente> GetCliente(int id)
        {
            return await dataContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await dataContext.Clientes.ToListAsync();
        }

        public async Task<Cliente> UpdateCliente(Cliente cliente)
        {
            var result = await dataContext.Clientes.FirstOrDefaultAsync(x => x.Id == cliente.Id);

            if (result != null)
            {
                result.Nome = cliente.Nome;
                result.Email = cliente.Email;
                result.Endereco = cliente.Endereco;
                result.Telefone = cliente.Telefone;
                result.UserId = cliente.UserId;
            }
            await dataContext.SaveChangesAsync();
            return result;
        }
        public async void DeleteCliente(int id)
        {
            var result = await dataContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                dataContext.Remove(result);
                await dataContext.SaveChangesAsync();
            };
        }
    }
}

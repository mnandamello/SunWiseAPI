using SunWiseAPI.Models;

namespace SunWiseAPI.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientes();
        Task<Cliente> GetCliente(int id);
        Task<Cliente> AddCliente(Cliente cliente);
        Task<Cliente> UpdateCliente(Cliente cliente);
        void DeleteCliente(int id);
    }
}

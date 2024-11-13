using SunWiseAPI.Models;

namespace SunWiseAPI.Repositories
{
    public interface IProjetoRepository
    {
        Task<IEnumerable<Projeto>> GetProjetos();
        Task<Cliente> GetProjeto(int id);
        Task<Cliente> AddProjeto(Cliente cliente);
        void DeleteProjeto(int id);
    }
}

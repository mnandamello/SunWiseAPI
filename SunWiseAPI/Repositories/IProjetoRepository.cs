using SunWiseAPI.Models;

namespace SunWiseAPI.Repositories
{
    public interface IProjetoRepository
    {
        Task<IEnumerable<Projeto>> GetProjetos();
        Task<Projeto> GetProjetoById(int id);
        Task <List<Projeto>> GetProjetoByUserId(string id);
        Task<Projeto> AddProjeto(Projeto projeto);
        void DeleteProjeto(int id);
    }
}

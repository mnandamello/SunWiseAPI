using SunWiseAPI.Models;

namespace SunWiseAPI.Repositories
{
    public interface IProjetoRepository
    {
        Task<IEnumerable<Projeto>> GetProjetos();
        Task<Cliente> GetProjetoById(int id);
        Task<Cliente> AddProjeto(Cliente cliente);
        //ta faltando o metodo das contas, q vai receber os valores de parametro e vai devolver os resultados das contas
        void DeleteProjeto(int id);
    }
}

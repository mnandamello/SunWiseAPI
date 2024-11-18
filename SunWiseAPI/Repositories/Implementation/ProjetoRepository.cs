using Microsoft.EntityFrameworkCore;
using SunWiseAPI.Data;
using SunWiseAPI.Models;

namespace SunWiseAPI.Repositories.Implementation
{
    public class ProjetoRepository : IProjetoRepository
    {

        private readonly IUserRepository userRepository;
        private readonly DataContext dataContext;

        public ProjetoRepository(IUserRepository userRepository, DataContext dataContext)
        {
            this.userRepository = userRepository;
            this.dataContext = dataContext;
        }
        public async Task<Projeto> AddProjeto(Projeto projeto)
        {
            var user = userRepository.GetUserById(projeto.UserId);
            if (user == null) return null;

            CalcularCamposProjeto(projeto);

            var result = await dataContext.AddAsync(projeto);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Projeto> GetProjetoById(int id)
        {
            return await dataContext.Projetos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Projeto>> GetProjetoByUserId(string id)
        {
            return await dataContext.Projetos.Where(u => u.UserId == id).ToListAsync();
        }

        public async Task<IEnumerable<Projeto>> GetProjetos()
        {
            return await dataContext.Projetos.ToListAsync();
        }

        public async void DeleteProjeto(int id)
        {
            
            var result = await dataContext.Projetos.FirstOrDefaultAsync(p => p.Id == id);

            if (result != null)
            {
                dataContext.Remove(result);
                await dataContext.SaveChangesAsync();
            }


        }

        private void CalcularCamposProjeto(Projeto projeto)
        {
            int taxaEnel = 50;
            double economiaMensal = projeto.TarifaEnergia - taxaEnel;
            projeto.EconomiaMensal = economiaMensal;

            int tempoRetornoMeses = (int)Math.Ceiling(projeto.Orcamento / economiaMensal);
            projeto.RetornoInvestimentoMeses = tempoRetornoMeses;

            int anos = tempoRetornoMeses / 12;
            int meses = tempoRetornoMeses % 12;
            projeto.RetornoEmAnos = $"{anos} anos e {meses} meses";

            double economia10Anos = economiaMensal * 12 * 10;
            projeto.EconomiaAcumulada10Anos = economia10Anos;

            double precoKwh = 0.50;
            double consumoEnergetico = projeto.TarifaEnergia / precoKwh;
            double co2Evitado10Anos = consumoEnergetico * 0.4 * 12 * 10;
            projeto.Co2Evitado10Anos = co2Evitado10Anos;

            projeto.ImpactoAmbiental = $"Em 10 anos, o cliente evitará a emissão de aproximadamente {co2Evitado10Anos / 1000:F2} toneladas de CO₂.";
        }
    }
}

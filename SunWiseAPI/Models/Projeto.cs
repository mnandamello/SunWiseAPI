using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SunWiseAPI.Models
{
    public class Projeto
    {
        [Key]
        public int Id { get; set; }
        public string NomeProjeto { get; set; }

        public double Orcamento { get; set; }

        public double TarifaEnergia { get; set; }

        public double? EconomiaMensal { get; set; }

        public int? RetornoInvestimentoMeses { get; set; }
        public double? EconomiaAcumulada10Anos { get; set; }

        public string? RetornoEmAnos { get; set; }

        public string? ImpactoAmbiental { get; set; }

        public double? Co2Evitado10Anos { get; set; }

        public int ClienteId { get; set; }
        public string UserId { get; set; }
    }
}

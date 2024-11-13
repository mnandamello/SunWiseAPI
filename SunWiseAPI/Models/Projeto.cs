using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace SunWiseAPI.Models
{
    public class Projeto
    {
        [Key]
        public int Id { get; set; }
        public string NomeProjeto { get; set; }
        public float Orçamento { get; set; }
        [Required]
        [Column(TypeName = "NUMBER(10,2)")]
        public decimal ConsumoMensalKwh { get; set; }

        [Required]
        [Column(TypeName = "NUMBER(10,2)")]
        public decimal TarifaEnergia { get; set; }

        [Column(TypeName = "NUMBER(10,2)")]
        public decimal? EconomiaMensal { get; set; }

        public int? RetornoInvestimentoMeses { get; set; }

        [Column(TypeName = "NUMBER(10,2)")]
        public decimal? EconomiaAcumulada10Anos { get; set; }

        public string ImpactoAmbiental { get; set; }

        public int ClienteId { get; set; }
        public string UserId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Milles_Project1Library.Interfaces.ModelsInterface;

namespace Milles_Project1Library.Models
{
    public class Calculator : ICalculator
    {
        [Key]
        public int CalculationId { get; set; }

        public decimal Number1 { get; set; }
        public decimal? Number2 { get; set; }

        public string Operator { get; set; }

        public decimal? Result { get; set; }

        public DateTime CalculationDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

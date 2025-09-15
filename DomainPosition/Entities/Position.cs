using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Position
    {
        public Guid Id { get; set; }                // PK
        public string PositionId { get; set; }     // Identificador lógico (agrupamento)
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public decimal Value { get; set; }

        [Key, Column(Order = 1)]
        public DateTime Date { get; set; }         // Timestamp da medição/posição
        public decimal Quantity { get; set; }


    }

}

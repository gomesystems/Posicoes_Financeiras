namespace Api.Dto
{
    public class PositionDto
    {
        public Guid Id { get; set; }
        public string PositionId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }

    public class ProductSummaryDto
    {
        public int ProductId { get; set; }
        public decimal TotalValue { get; set; }
    }
}

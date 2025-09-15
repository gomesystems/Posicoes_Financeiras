using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionRepository _repo;
        public PositionsController(IPositionRepository repo) => _repo = repo;

        // GET /api/positions/client/{clientId}
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetByClient(int clientId, CancellationToken ct)
        {
            var positions = await _repo.GetLatestPositionsByClientAsync(clientId, ct);

            var dto = positions.Select(p => new PositionDto
            {
                Id = p.Id,
                PositionId = p.PositionId,
                ClientId = p.ClientId,
                ProductId = p.ProductId,
                Value = p.Value,
                Date = p.Date
            }).ToList();

            return Ok(dto);
        }

        // GET /api/positions/client/{clientId}/summary
        [HttpGet("client/{clientId}/summary")]
        public async Task<IActionResult> GetSummaryByClient(int clientId, CancellationToken ct)
        {
            var summary = await _repo.GetProductSummaryForClientAsync(clientId, ct);

            var dto = summary.Select(s => new ProductSummaryDto
            {
                ProductId = s.ProductId,
                TotalValue = s.TotalValue
            }).ToList();

            return Ok(dto);
        }

        // GET /api/positions/top10
        [HttpGet("top10")]
        public async Task<IActionResult> GetTop10(CancellationToken ct)
        {
            var top10 = await _repo.GetTop10LatestPositionsByValueAsync(ct);

            var dto = top10.Select(p => new PositionDto
            {
                Id = p.Id,
                PositionId = p.PositionId,
                ClientId = p.ClientId,
                ProductId = p.ProductId,
                Value = p.Value,
                Date = p.Date
            }).ToList();

            return Ok(dto);
        }
    }
}

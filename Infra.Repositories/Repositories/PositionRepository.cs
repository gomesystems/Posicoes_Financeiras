using Domain.Entities;
using Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Repositories
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetLatestPositionsByClientAsync(int clientId, CancellationToken ct = default);
        Task<List<(int ProductId, decimal TotalValue)>> GetProductSummaryForClientAsync(int clientId, CancellationToken ct = default);
        Task<List<Position>> GetTop10LatestPositionsByValueAsync(CancellationToken ct = default);
    }

    public class PositionRepository : IPositionRepository
    {
        private readonly AppDbContext _db;
        public PositionRepository(AppDbContext db) => _db = db;

        // 1) Para um clientId, pegar a posição mais recente por positionId
        public async Task<List<Position>> GetLatestPositionsByClientAsync(int clientId, CancellationToken ct = default)
        {
            // Subquery: para cada PositionId pegar max(Date) filtrando pelo client
            var lastPerPosition = _db.Positions
                .AsNoTracking()
                .Where(p => p.ClientId == clientId)
                .GroupBy(p => p.PositionId)
                .Select(g => new
                {
                    PositionId = g.Key,
                    MaxDate = g.Max(x => x.Date)
                });

            // Join com a tabela positions para recuperar as colunas completas
            var query = from p in _db.Positions.AsNoTracking()
                        join l in lastPerPosition on new { p.PositionId, p.Date } equals new { l.PositionId, Date = l.MaxDate }
                        where p.ClientId == clientId
                        select p;

            return await query.ToListAsync(ct);
        }

        // 2) Mesma ideia: pegar as últimas posições por positionId, agrupar por productId e somar Value
        public async Task<List<(int ProductId, decimal TotalValue)>> GetProductSummaryForClientAsync(int clientId, CancellationToken ct = default)
        {
            var lastPerPosition = _db.Positions
                .AsNoTracking()
                .Where(p => p.ClientId == clientId)
                .GroupBy(p => p.PositionId)
                .Select(g => new
                {
                    PositionId = g.Key,
                    MaxDate = g.Max(x => x.Date)
                });

            var latestPositions = from p in _db.Positions.AsNoTracking()
                                  join l in lastPerPosition on new { p.PositionId, p.Date } equals new { l.PositionId, Date = l.MaxDate }
                                  where p.ClientId == clientId
                                  select new { p.ProductId, p.Value };

            var summary = await latestPositions
                .GroupBy(x => x.ProductId)
                .Select(g => new { ProductId = g.Key, TotalValue = g.Sum(x => x.Value) })
                .ToListAsync(ct);

            return summary.Select(s => (s.ProductId, s.TotalValue)).ToList();
        }

        // 3) Top 10: pegar último por positionId em todo o conjunto, ordenar por Value desc e pegar 10
        public async Task<List<Position>> GetTop10LatestPositionsByValueAsync(CancellationToken ct = default)
        {
            var lastPerPosition = _db.Positions
                .AsNoTracking()
                .GroupBy(p => p.PositionId)
                .Select(g => new
                {
                    PositionId = g.Key,
                    MaxDate = g.Max(x => x.Date)
                });

            var latestPositions = from p in _db.Positions.AsNoTracking()
                                  join l in lastPerPosition on new { p.PositionId, p.Date } equals new { l.PositionId, Date = l.MaxDate }
                                  select p;

            var top10 = await latestPositions
                        .OrderByDescending(p => p.Value)
                        .ThenByDescending(p => p.Date)
                        .Take(10)
                        .ToListAsync(ct);

            return top10;
        }
    }
}

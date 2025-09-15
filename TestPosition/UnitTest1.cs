using Domain.Entities;
using Infra.Data.Data;
using Infra.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;


namespace TestPosition
{

    private AppDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // banco único por teste
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetLatestPositionsByClientAsync_ReturnsLatestPerPosition()
    {
        // Arrange
        using var db = CreateInMemoryDb();

        db.Positions.AddRange(new List<Position>
        {
            new Position { Id = 1, ClientId = 100, PositionId = "A", Date = new DateTime(2025, 1, 1), Value = 10 },
            new Position { Id = 2, ClientId = 100, PositionId = "A", Date = new DateTime(2025, 2, 1), Value = 20 }, // último de A
            new Position { Id = 3, ClientId = 100, PositionId = "B", Date = new DateTime(2025, 1, 15), Value = 30 },
            new Position { Id = 4, ClientId = 100, PositionId = "B", Date = new DateTime(2025, 3, 1), Value = 40 }, // último de B
            new Position { Id = 5, ClientId = 200, PositionId = "A", Date = new DateTime(2025, 4, 1), Value = 50 }, // outro cliente
        });

        await db.SaveChangesAsync();


        //usar service
        var service = new PositionRepository(db);

        // Act
        var result = await service.GetLatestPositionsByClientAsync(100);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count); // A e B

        var positionA = result.Single(p => p.PositionId == "A");
        var positionB = result.Single(p => p.PositionId == "B");

        Assert.Equal(20, positionA.Value); // último valor de A
        Assert.Equal(40, positionB.Value); // último valor de B
    }
}
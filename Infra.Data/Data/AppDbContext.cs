using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infra.Data.Data;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Position> Positions { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Position>(b =>
        //    {
        //        b.ToTable("positions");
        //        b.HasKey(x => x.Id);
        //        b.Property(x => x.PositionId).HasMaxLength(100).IsRequired();
        //        b.Property(x => x.Value).HasColumnType("numeric(18,4)");
        //        b.Property(x => x.Date).IsRequired();

        //        // Sugeridos índices para consulta eficiente
        //        b.HasIndex(p => new { p.ClientId, p.PositionId, p.Date });
        //        b.HasIndex(p => new { p.PositionId, p.Date });
        //        b.HasIndex(p => p.ProductId);
        //    });
        //}
    
}
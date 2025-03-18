// src/4.FloorplanMaskedArea/Data/TrackingBleDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace TrackingBle.Data
{
    public class TrackingBleDbContext : DbContext
    {
        public TrackingBleDbContext(DbContextOptions<TrackingBleDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Kosongkan atau biarkan minimal, karena migrations akan mengambil definisi dari file
            base.OnModelCreating(modelBuilder);
        }
    }
}
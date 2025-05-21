
using Microsoft.EntityFrameworkCore;

namespace SigesaData.Context
{
    public class BitacoraContext : DbContext
    {
        public BitacoraContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("DBSIGESA");

            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}

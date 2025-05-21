

using Microsoft.EntityFrameworkCore;

namespace SigesaData.Context
{
    public class ReservaContext : DbContext
    {
        public ReservaContext(DbContext options): base () { }
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

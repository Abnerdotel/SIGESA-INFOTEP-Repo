

using Microsoft.EntityFrameworkCore;

namespace SigesaData.Context
{
    public class EquipamientoContext : DbContext
    {
        public EquipamientoContext(DbContext options ): base () { }

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

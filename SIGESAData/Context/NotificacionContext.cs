
using Microsoft.EntityFrameworkCore;

namespace SigesaData.Context
{
    public class NotificacionContext : DbContext
    {
        public NotificacionContext(DbContext options): base() { }

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

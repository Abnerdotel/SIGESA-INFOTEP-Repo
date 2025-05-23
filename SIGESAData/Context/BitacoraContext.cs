﻿
using Microsoft.EntityFrameworkCore;
using SigesaEntidades;

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

        public DbSet<Bitacora> Bitacoras { get; set; }
    }
}

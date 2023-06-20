using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebApiParanaBanco.Models;

namespace WebApiParanaBanco.Repositorio
{
    public class ClienteContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Telefones)
                .WithOne()
                .HasForeignKey(t => t.Numero);
        }
    }

}

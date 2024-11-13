using Microsoft.EntityFrameworkCore;
using SunWiseAPI.Models;

namespace SunWiseAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Projeto> Projetos { get; set; }

    }
}
